using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfrroStock.Services
{
    public class MachineLearning
    {
        private readonly string[] collections = { "Food", "Nature", "Fashion", "People", "Mockup", "Plants" };
        private readonly string[] categories = { "War", "Concert", "Film", "Dancing", "Baby", "Nurses" };
        private readonly string[] tags = { "yellow", "red", "playful", "water", "nature", "design", "powerpoint", "child"  };
        private static readonly Random rand = new Random();

        public MachineLearning()
        {

        }

        public (string collection, string category, string[] tags) Pipeline()
        {
            var collection = collections[rand.Next(1, collections.Length)];
            var category = categories[rand.Next(1, categories.Length)];
            var tagIndex = rand.Next(1, tags.Length);
            List<string> selectedTags = new List<string>();
            for(int index = 0; index < tagIndex; index++)
            {
                var randIdx = rand.Next(1, tags.Length);
                selectedTags.Add(tags[randIdx]);
            }
            return (collection, category, selectedTags.ToArray());

        }
        
    }
}
