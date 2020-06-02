import React from "react";
import { ConnectedSearchArea } from "../SearchArea/SearchArea";
import "./_Header.scss";

export default function Header() {
  return (
    <div className="afro_header">
      <div className="hero">
        <img
          className="img-fluid"
          src="https://images.pexels.com/photos/981286/pexels-photo-981286.jpeg"
          alt="beautiful leaves as header background"
        />
      </div>
      <div className="m0-auto header-width header-search">
        <h1 className="hero__title text-white">
          {
            "Explore thousands of royalty free stock photos & media from around Africa"
          }
        </h1>
      </div>
      <ConnectedSearchArea
        type="head"
        searchClass="header-search header-width"
      />
    </div>
  );
}
