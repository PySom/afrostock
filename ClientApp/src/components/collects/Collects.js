import React, { useState, useEffect } from "react";
import CollectPhotoGrid from "../PhotoGrid/CollectPhotoGrid";
import api from "../../sideEffects/apis/api";
import { connect } from "react-redux";
import { useHistory } from "react-router-dom";
import "./_Collects.scss";

function Collect(props) {
  const [collectImages, setCollectImages] = useState(null);
  const [singleBatch, setSingleBatch] = useState([]);
  const [batch_, setBatch] = useState(0);
  const [hasmore, sethasmore] = useState(false);
  console.log(collectImages);
  const fetchMoreData = () => {
    setBatch(batch_ + 20);
    const singleBatch__ = collectImages.splice(batch_, 20);

    if (singleBatch__.length != 0) {
      setSingleBatch(singleBatch.concat(singleBatch__));
      sethasmore(true);
    }
  };
  useEffect(() => {
    const extractImages = JSON.parse(localStorage.getItem("collect")).map(
      (item) => item.image
    );
    setCollectImages(extractImages);
    const singleBatch_ = extractImages.splice(batch_, 20);
    setSingleBatch(singleBatch_);
  }, []);

  return (
    <>
      <div className="homeWrapper"></div>
      <div className="discover">
        <div className="container-fluid mt-20">
          <div className="author__wrap">
            <div className="row">
              <div className="col-md-12 text-center">
                <h2>Collection Name</h2>
                <div className="collections">
                  <span className="caption">
                    59 photos and 2 videos collected by
                  </span>
                  <span className="author">isa Fotios</span>
                </div>
              </div>
            </div>
          </div>
          {collectImages && (
            <CollectPhotoGrid
              dataLength={singleBatch.length}
              fetch_={fetchMoreData}
              contents={singleBatch}
              hasmore_={hasmore}
            />
          )}
        </div>
      </div>
    </>
  );
}

const mapStateToProps = ({ collects }) => {
  return {
    collects,
  };
};
export default connect(mapStateToProps, null)(Collect);
