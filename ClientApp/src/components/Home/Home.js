import React, { useState, useEffect } from "react";
import Header from "../Header/Header";
import DropDown from "../Dropdown/DropDown";
import PhotoGrid from "../PhotoGrid/PhotoGrid";
import api from "../../sideEffects/apis/api";
import MainBody from "../MainBody/MainBody";

export default function Home(props) {
  const [contents, setContents] = useState([]);
  const [page, setPage] = useState(1);
  const [pageLoaded, setPageLoaded] = useState(true);

  useEffect(() => {
    if (pageLoaded) {
      api
        .getAll(`images?page=${page}`, "images")
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

  console.log(contents);
  const fetchMoreData = () => {
    setPage(page + 1);
    api
      .getAll(`images?page=${2}`, "images")
      .then((response) => {
        setContents(contents.concat(response));
        setPageLoaded(false);
      })
      .catch((err) => {
        console.log(err);
        setPageLoaded(false);
      });
  };

  return (
    <>
      <div className="homeWrapper"></div>
      <div className="discover">
        <div className="header-bg pt-1h pb-1h plr-15">
          <Header />
        </div>
        <MainBody>
          <div className="container-fluid trend-pos mt-20">
            <h4 className="app-font-mid">Free Stock Photos</h4>
            <DropDown />
          </div>
          <div className="container-fluid mt-20">
            <PhotoGrid
              dataLength={contents.length}
              fetch_={fetchMoreData}
              contents={contents}
            />
          </div>
        </MainBody>
      </div>
    </>
  );
}
