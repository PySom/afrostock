import React from "react";
import "./_Modal.scss";

export default function ModalCustomBody({
  src,
  contentType,
  name,
  description,
  tags,
}) {
  console.log({ src, contentType, name, description });
  return (
    <div className="container-fluid">
      <div className="text-center">
        {contentType === 0 && (
          <figure
            className="r-p"
            style={{
              backgroundImage: `url(${src})`,
              backgroundSize: "contain",
              backgroundRepeat: "no-repeat",
              backgroundPosition: "center",
            }}
          >
            <img
              style={{ visibility: "hidden" }}
              src={src}
              alt={name}
              title={description || name}
              className="img-fluid"
            />
          </figure>
        )}

        {contentType === 1 && (
          <video oncontextmenu={`return ${false};`} controls src={src}></video>
        )}
      </div>
    </div>
  );
}
