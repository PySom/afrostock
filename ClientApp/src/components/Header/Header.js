import React from "react";
import { ConnectedSearchArea } from "../SearchArea/SearchArea";
import "./_Header.scss";

export default function Header() {
  return (
    <div className="afro_header">
      <div className="hero">
        <img
          className="img-fluid"
          src="images/hero.jpg"
          alt="beautiful leaves as header background"
        />
      </div>
      <div className="m0-auto header-width header-search">
        <h1 className="hero__title text-white">
          {
            "Explore thousands of royalty stock photos & media from around Africa"
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
