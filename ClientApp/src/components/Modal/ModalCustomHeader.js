import React from "react";
import "./_Modal.scss";

export default function ModalCustomHeader({ authorName, views, onClick }) {
  return (
    <div className="app-flex">
      <img className="img-fluid" src="images/avatar.png" alt="user_profile" />
      <p>{authorName}</p>
      <div className="app-flex pull-right">
        <div className="mr-4">
          <button>
            <span className="fa fa-eye mr-1"></span>
            {views}
          </button>
        </div>
        <div className="download-button">
          <button type="button" onClick={onClick}>
            Download
          </button>
        </div>
      </div>
    </div>
  );
}
