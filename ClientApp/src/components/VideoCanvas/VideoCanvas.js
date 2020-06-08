import React, { createRef, useState, useEffect } from "react";

export default function VideoCanvas({ src }) {
  const [showCanvas, setShowCanvas] = useState(true);
  const canvasRef = createRef();
  const [pageLoaded, setPageLoaded] = useState(false);

  useEffect(() => {
      setPageLoaded(true);
  }, [pageLoaded]);

  return <>{pageLoaded && <canvas ref={canvasRef}></canvas>}</>;
}
