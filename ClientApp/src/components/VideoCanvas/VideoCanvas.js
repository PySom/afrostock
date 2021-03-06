﻿import React, { createRef, useState, useEffect } from "react";

export default function VideoCanvas({ src }) {
  const [showCanvas, setShowCanvas] = useState(true);
  const canvasRef = createRef();
  const [pageLoaded, setPageLoaded] = useState(true);

  useEffect(() => {
    var video = document.createElement("video");
    var c = canvasRef.current;
    var ctx = c.getContext("2d");

    document.body.appendChild(video);
    document.body.appendChild(c);

    video.addEventListener(
      "loadeddata",
      function () {
        c.width = video.videoWidth;
        c.height = video.videoHeight;
      },
      false
    );

    video.preload = "auto";
    video.src = "https://media.w3.org/2010/05/sintel/trailer.mp4";
    video.controls = true;
    video.controlsList = "nodownload";

    var draw = () => {
      ctx.drawImage(video, 0, 0, c.width, c.height);
      //edit the image here
      //requestAnimationFrame(draw);
    };

    requestAnimationFrame(draw);
  }, []);

  return <>{pageLoaded && <canvas ref={canvasRef}></canvas>}</>;
}

// console.log("the true ref is", canvasRef);
// const mediaSource = src;
// let muted = true;
// const canvas = canvasRef.current; // get the canvas from the page
// const ctx = canvas.getContext("2d");
// let videoContainer; // object to hold video and associated info
// const video = document.createElement("video"); // create a video element
// video.src = mediaSource;
// // the video will now begin to load.
// // As some additional info is needed we will place the video in a
// // containing object for convenience
// video.autoPlay = false; // ensure that the video does not auto play
// video.loop = true; // set the video to loop.
// video.muted = muted;
// videoContainer = {
//   // we will add properties as needed
//   video: video,
//   ready: false,
// };
// // To handle errors. This is not part of the example at the moment. Just fixing for Edge that did not like the ogv format video
// video.onerror = function (e) {
//   setShowCanvas(true);
// };

// video.oncanplay = readyToPlayVideo; // set the event to the play function that
// // can be found below

// function readyToPlayVideo(event) {
//   // this is a referance to the video
//   // the video may not match the canvas size so find a scale to fit
//   videoContainer.scale = Math.min(
//     canvas.width / this.videoWidth,
//     canvas.height / this.videoHeight
//   );
//   videoContainer.ready = true;
//   // the video can be played so hand it off to the display function
//   requestAnimationFrame(updateCanvas);
//   // add instruction
//   //document.getElementById("playPause").textContent = "Click video to play/pause.";
//   //document.querySelector(".mute").textContent = "Mute";
// }

// function updateCanvas() {
//   ctx.clearRect(0, 0, canvas.width, canvas.height);
//   // only draw if loaded and ready
//   if (videoContainer !== undefined && videoContainer.ready) {
//     // find the top left of the video on the canvas
//     video.muted = muted;
//     var scale = videoContainer.scale;
//     var vidH = videoContainer.video.videoHeight;
//     var vidW = videoContainer.video.videoWidth;
//     var top = canvas.height / 2 - (vidH / 2) * scale;
//     var left = canvas.width / 2 - (vidW / 2) * scale;
//     // now just draw the video the correct size
//     ctx.drawImage(
//       videoContainer.video,
//       left,
//       top,
//       vidW * scale,
//       vidH * scale
//     );
//     if (videoContainer.video.paused) {
//       // if not playing show the paused screen
//       drawPayIcon();
//     }
//   }
//   // all done for display
//   // request the next frame in 1/60th of a second
//   requestAnimationFrame(updateCanvas);
// }

// function drawPayIcon() {
//   ctx.fillStyle = "black"; // darken display
//   ctx.globalAlpha = 0.5;
//   ctx.fillRect(0, 0, canvas.width, canvas.height);
//   ctx.fillStyle = "#DDD"; // colour of play icon
//   ctx.globalAlpha = 0.75; // partly transparent
//   ctx.beginPath(); // create the path for the icon
//   var size = (canvas.height / 2) * 0.5; // the size of the icon
//   ctx.moveTo(canvas.width / 2 + size / 2, canvas.height / 2); // start at the pointy end
//   ctx.lineTo(canvas.width / 2 - size / 2, canvas.height / 2 + size);
//   ctx.lineTo(canvas.width / 2 - size / 2, canvas.height / 2 - size);
//   ctx.closePath();
//   ctx.fill();
//   ctx.globalAlpha = 1; // restore alpha
// }

// function playPauseClick() {
//   if (videoContainer !== undefined && videoContainer.ready) {
//     if (videoContainer.video.paused) {
//       videoContainer.video.play();
//     } else {
//       videoContainer.video.pause();
//     }
//   }
// }
// function videoMute() {
//   muted = !muted;
//   if (muted) {
//     document.querySelector(".mute").textContent = "Mute";
//   } else {
//     document.querySelector(".mute").textContent = "Sound on";
//   }
// }
// // register the event

// document.querySelector("canvas").addEventListener("click", playPauseClick);
// //document.querySelector(".mute").addEventListener("click", videoMute);
// }, []);

// // Original source has returns 404
// // var mediaSource = "http://video.webmfiles.org/big-buck-bunny_trailer.webm";
// // New source from wiki commons. Attribution in the leading credits.
