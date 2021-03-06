﻿import React, { useState, useEffect } from "react";
import PhotoGrid from "../PhotoGrid/PhotoGrid";
import api from "../../sideEffects/apis/api";
import ls from "../../sideEffects/local/ourLocalStorage";
import VideoCanvas from "../VideoCanvas/VideoCanvas";
import { connect } from "react-redux";
import { setLoader } from "../../creators/loaderCreator";
import { setVisibleState } from "../../creators/visbleSearchCreator";

function VideoPage({ match, setLoader, setVisibleState }) {
  const [pageLoaded, setPageLoaded] = useState(true);
  const [contents, setContents] = useState([]);
  console.log({ contents }, "I came here again");

  useEffect(() => {
    setVisibleState(false);
  }, []);
  useEffect(() => {
    if (pageLoaded) {
      console.log({ match });
      let url = null;
      if (match) {
        let recent = ls.getItemInLs("recent");
        if (recent) {
          recent = recent.concat(match.params.id);
          recent = [...new Set(recent)];
        } else {
          recent = [match.params.id];
        }
        ls.persistItemInLS("recent", recent);
        url = `images/videos/searchfor/?term=${match.params.id}`;
      } else {
        url = "images/videos";
      }
      api
        .getAll(url)
        .then((response) => {
          setContents(response);
          setPageLoaded(false);
          setLoader(false);
        })
        .catch((err) => {
          console.log(err);
          setPageLoaded(false);
        });
    }
  }, [pageLoaded, match]);
  return (
    <>
      {match && (
        <h4 className="text-center text-title mt-4 font-title">
          {match.params.id} Contents
        </h4>
      )}
      <div className="container-fluid">
        <PhotoGrid dataLength={contents.length} contents={contents} />
      </div>
    </>
  );
}

//get props value
const matchStateToProps = ({ searchVisibility }) => {
  return { searchVisibility };
};

export default connect(null, { setVisibleState, setLoader })(VideoPage);
