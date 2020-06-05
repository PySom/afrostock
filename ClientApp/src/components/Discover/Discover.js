﻿import React, { useState, useEffect } from "react";
import Collection from "../Collection/Collection";
import { useHistory } from "react-router-dom";
import { Row, Col } from "reactstrap";
import "./_Discover.scss";
import api from "../../sideEffects/apis/api";
import { setCollectData } from "../../creators/collectsCreator";
import { setLoader } from "../../creators/loaderCreator";
import { connect } from "react-redux";

function Discover(props) {
  const [pageLoaded, setPageLoaded] = useState(true);
  const [contents, setContents] = useState([]);
  const history = useHistory();
  useEffect(() => {
    if (pageLoaded) {
      api
        .getAll("collectiontypes", "collectiontypes")
        .then((response) => {
          console.log(response);
          setContents(response);
          props.setLoader(false);
          setPageLoaded(false);
        })
        .catch((err) => {
          console.log(err);
          setPageLoaded(false);
        });
    }
  }, [pageLoaded]);
  console.log(contents);

  const showCollects = (data, id) => {
    history.push(`/discover/${id}`);
    props.setCollectData(data);
    localStorage.setItem("collect", JSON.stringify(data));
  };

  return (
    <div className="container">
      {contents.map((collectionType) => (
        <div
          key={collectionType.name + collectionType.id}
          className="collection__wrapper"
        >
          <Row className="mt-4">
            <Col>
              <h2 className="title">{collectionType.name}</h2>
            </Col>
          </Row>
          {collectionType.collections.length > 3 && (
            <Row className="mt-2 mb-3">
              <Col>
                <div className="collection__buttons">
                  {collectionType.collections
                    .slice(3)
                    .map((collection, index) => (
                      <div
                        key={collection.name + collection.id}
                        className={index === 0 ? "mr-1" : "mr-2"}
                      >
                        <button
                          className="discover-cat"
                          onClick={() =>
                            showCollects(collection.collectibles, collection.id)
                          }
                        >
                          {collection.name}
                        </button>
                      </div>
                    ))}
                </div>
              </Col>
            </Row>
          )}

          <Row className="mt-4 mb-4 pad__collections">
            {collectionType.collections.slice(0, 3).map((collection) => (
              <Col key={collection.name + collection.id} sm={12} md={4} lg={4}>
                <Collection
                  collectibles={collection.collectibles}
                  id={collection.id}
                  name={collection.name}
                />
              </Col>
            ))}
          </Row>
        </div>
      ))}
    </div>
  );
}

export default connect(null, { setCollectData, setLoader })(Discover);
