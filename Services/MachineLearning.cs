using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.ML;
using AfrroStock.Models.ML;
using AfrroStock.Services.YoloParser;
using Microsoft.AspNetCore.Hosting;

namespace AfrroStock.Services
{
    public class MachineLearning
    {
        private readonly string[] categories = { "War", "Concert", "Film", "Dancing", "Baby", "Nurses" };
        private readonly string[] tags = { "yellow", "red", "playful", "water", "nature", "design", "powerpoint", "child"  };
        private static readonly Random rand = new Random();

        public MachineLearning()
        {

        }

        public string GetAbsolutePath(string relativePath)
        {
            var _dataRoot = Path.Combine(Environment.CurrentDirectory, "ML");

            string fullPath = Path.Combine(_dataRoot, relativePath);

            return fullPath;
        }

        public List<List<Predict>> Pipe(string imgPath, IWebHostEnvironment _env)
        {
            string assetsPath = GetAbsolutePath("assets");
            var modelFilePath = Path.Combine(assetsPath, "Model", "Model.onnx");
            var absolutePath = Path.Combine(_env.WebRootPath, "assets", imgPath);
            var imagesFolder = Path.Combine(assetsPath, "images");
            var outputFolder = Path.Combine(assetsPath, "images", "output");

            var predicted = new List<List<Predict>>();

            MLContext mlContext = new MLContext();
            try
            {
                ImageNetData[] img = new ImageNetData[] { new ImageNetData { ImagePath = absolutePath, Label = imgPath } };
                IEnumerable<ImageNetData> images = ImageNetData.ReadFromFile(imagesFolder);
                IDataView imageDataView = mlContext.Data.LoadFromEnumerable(img);

                var modelScorer = new OnnxModelScorer(imagesFolder, modelFilePath, mlContext);

                // Use model to score data
                IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

                YoloOutputParser parser = new YoloOutputParser();

                var boundingBoxes =
                    probabilities
                    .Select(probability => parser.ParseOutputs(probability))
                    .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

                for (var i = 0; i < images.Count(); i++)
                {
                    string imageFileName = images.ElementAt(i).Label;
                    IList<YoloBoundingBox> detectedObjects = boundingBoxes.ElementAt(i);

                    //DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);
                    predicted.Add(LogDetectedObjects(imageFileName, detectedObjects));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("========= End of Process..Hit any Key ========");
            //Console.ReadLine();

            return predicted;
        }

        private void DrawBoundingBox(string inputImageLocation, string outputImageLocation, string imageName, IList<YoloBoundingBox> filteredBoundingBoxes)
        {
            Image image = Image.FromFile(Path.Combine(inputImageLocation, imageName));

            var originalImageHeight = image.Height;
            var originalImageWidth = image.Width;
            foreach (var box in filteredBoundingBoxes)
            {
                var x = (uint)Math.Max(box.Dimensions.X, 0);
                var y = (uint)Math.Max(box.Dimensions.Y, 0);
                var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                x = (uint)originalImageWidth * x / OnnxModelScorer.ImageNetSettings.imageWidth;
                y = (uint)originalImageHeight * y / OnnxModelScorer.ImageNetSettings.imageHeight;
                width = (uint)originalImageWidth * width / OnnxModelScorer.ImageNetSettings.imageWidth;
                height = (uint)originalImageHeight * height / OnnxModelScorer.ImageNetSettings.imageHeight;

                string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

                using Graphics thumbnailGraphic = Graphics.FromImage(image);
                thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Define Text Options
                Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                SolidBrush fontBrush = new SolidBrush(Color.Black);
                Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                // Define BoundingBox options
                Pen pen = new Pen(box.BoxColor, 3.2f);
                SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);

                thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

                // Draw bounding box on image
                thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
            }

            if (!Directory.Exists(outputImageLocation))
            {
                Directory.CreateDirectory(outputImageLocation);
            }

            image.Save(Path.Combine(outputImageLocation, imageName));

        }

        private List<Predict> LogDetectedObjects(string imageName, IList<YoloBoundingBox> boundingBoxes)
        {
            List<Predict> predict = new List<Predict>();
            Console.WriteLine($".....The objects in the image {imageName} are detected as below....");

            foreach (var box in boundingBoxes)
            {
                predict.Add(new Predict { Label = box.Label, Confidence = box.Confidence });
                Console.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
            }

            Console.WriteLine("");
            return predict;
        }

        public struct Predict 
        {
            public string Label { get; set; }
            public float Confidence { get; set; }
        }

        public (string category, string[] tags) Pipeline()
        {
            var category = categories[rand.Next(1, categories.Length)];
            var tagIndex = rand.Next(1, tags.Length);
            HashSet<string> selectedTags = new HashSet<string>();
            for(int index = 0; index < tagIndex; index++)
            {
                var randIdx = rand.Next(1, tags.Length);
                selectedTags.Add(tags[randIdx]);
            }
            return (category, selectedTags.ToArray());

        }
        
    }
}
