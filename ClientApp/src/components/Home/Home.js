import React, { useState, useEffect } from "react";
import Header from "../Header/Header";
import DropDown from "../Dropdown/DropDown";
import PhotoGrid from "../PhotoGrid/PhotoGrid";
import api from "../../sideEffects/apis/api";
import MainBody from "../MainBody/MainBody";

export default function Home(props) {
  const [contents, setContents] = useState([]);
  const [pageLoaded, setPageLoaded] = useState(true);

  useEffect(() => {
    if (pageLoaded) {
      api
        .getAll("images", "images")
        .then((response) => {
          setContents(response);
          setPageLoaded(false);
        })
        .catch((err) => {
          console.log(err);
          setPageLoaded(false);
        });
    }
  }, [pageLoaded]);

  return (
    <>
      <div className="homeWrapper">
        <img
          className="img-fluid hero"
          src="https://images.pexels.com/photos/2867904/pexels-photo-2867904.jpeg?auto=compress&cs=tinysrgb&fit=crop&h=250.0&w=1000"
          alt="background-image"
        />
      </div>
      <div className="discover">
        <div className="header-bg pt-1h pb-1h plr-15">
          <Header />
        </div>
        <MainBody>
          <div className="enforce-mp trend-pos mt-20">
            <p className="app-font-mid">Free Stock Photos</p>
            <DropDown />
          </div>
          <div className="enforce-mp mt-20">
            <PhotoGrid contents={contents} />
          </div>
        </MainBody>
      </div>
    </>
  );
}