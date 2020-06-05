import React, { useState, useEffect } from "react";
import CollectPhotoGrid from "../PhotoGrid/CollectPhotoGrid";
import api from "../../sideEffects/apis/api";
import { connect } from "react-redux";
import { useHistory } from "react-router-dom";
import { setLoader } from "../../creators/loaderCreator";
import "./_Collects.scss";
import history from "../history";
import Footer from "../Footer";

function Collect(props) {
  const [collectImages, setCollectImages] = useState(null);
  const [singleBatch, setSingleBatch] = useState([]);
  const [collectionData, setCollectionData] = useState(null);
  const [batch_, setBatch] = useState(0);
  const [hasmore, sethasmore] = useState(false);
  const [collectionName, setCollectionName] = useState("");
  // const [collectorName, setCollectorName] = useState("");
  const [allCollects, setAllCollects] = useState();
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
    // let collectorData = JSON.parse(localStorage.getItem("user"));
    // console.log("data", collectorData);
    // let name = collectorData.surName + " " + collectorData.firstName;
    // setCollectorName(name);
    let data = history.location.pathname.substring(10);
    const route = document.documentURI.substring(document.baseURI.length + 9);
    api
      .getAll(`collections/${Number(data)}`, `collections/${data}`)
      .then((response) => {
        setCollectionData(response);
        setCollectionName(response.name);
        const extractImages = response.collectibles.map((item) => item.image);
        setAllCollects(extractImages.length);
        setCollectImages(extractImages);
        const singleBatch_ = extractImages.splice(batch_, 20);
        setSingleBatch(singleBatch_);
        props.setLoader(false);
      })
      .catch((err) => console.log(err));

    if (localStorage.getItem(`collect/${Number(route)}`)) {
      const storedData = JSON.parse(
        localStorage.getItem(`collect/${Number(route)}`)
      );
      setCollectionName(localStorage.getItem("collectionName"));

      const extractImages = storedData.map((item) => item.image);
      setAllCollects(extractImages.length);
      setCollectImages(extractImages);
      const singleBatch_ = extractImages.splice(batch_, 20);
      setSingleBatch(singleBatch_);
    }
  }, []);

  console.log(collectImages, collectionData);
  return (
    <>
      <div className="homeWrapper"></div>
      <div className="discover">
        <div className="container-fluid mt-20">
          <div className="author__wrap">
            <div className="row">
              <div className="col-md-12 text-center">
                <h2>{collectionName && collectionName}</h2>
                <div className="collections">
                  <span className="caption">
                    {collectImages && `${allCollects} photos  collected by`}
                  </span>
                  <span className="author">Anonymous</span>
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
        <Footer />
      </div>
    </>
  );
}

const mapStateToProps = ({ collects }) => {
  return {
    collects,
  };
};
export default connect(mapStateToProps, { setLoader })(Collect);
