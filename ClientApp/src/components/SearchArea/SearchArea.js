import React, { useState, useEffect } from "react";
import SearchBar from "../SearchBar/SearchBar";
import ResultArea from "../SearchBar/ResultArea/ResultArea";
import VizSensor from "react-visibility-sensor";
import { connect } from "react-redux";
import api from "../../sideEffects/apis/api";
import { useHistory } from "react-router-dom";

export function SearchArea({ className, setVisibleState, searchClass, type }) {
  const history = useHistory();
  const [show, setShow] = useState(false);
  const [areaInView, setAreaInView] = useState(false);
  const [contents, setContents] = useState([]);
  const [searchValue, setSearchValue] = useState("");

  const onEnterPressed = () => {
    if (searchValue && searchValue.length > 2) {
      history.push(`/contents/${searchValue}`);
    }
  };

  const handleChange = (value) => {
    console.log(value);
    if (!show) setShow(true);
    setSearchValue(value);
  };

  useEffect(() => {
    if (searchValue && searchValue.length >= 3) {
      //api logic
      api
        .getAll(`images/search?term=${searchValue}`, "searchterm")
        .then((response) => {
          console.log({ response });
          setContents(response);
        })
        .catch((err) => {
          console.log(err);
        });
    }
  }, [searchValue]);

  const handleMouseOut = () => {
    setAreaInView(false);
  };

  const visualStateManager = (state) => {
    if (type === "head") {
      setVisibleState(state);
    }
  };

  return (
    <VizSensor onChange={(visible) => visualStateManager(visible)}>
      <div className={`${className ? className : ""}`}>
        <SearchBar
          onKeyPress={onEnterPressed}
          className={searchClass}
          searchValue={searchValue}
          setShow={setShow}
          show={show}
          onChange={handleChange}
          areaInView={areaInView}
        />
        <div
          className={`m0-auto ${searchClass ? searchClass : ""} r-p`}
          onClick={() => setShow(true)}
        >
          <ResultArea
            term={searchValue}
            results={contents}
            style={{ display: show ? "block" : "none" }}
            onMouseOver={() => setAreaInView(true)}
            onMouseOut={handleMouseOut}
          />
        </div>
      </div>
    </VizSensor>
  );
}

//Below defines actions and reducers
const actionTypes = {
  visible: "SEARCH_VISBLE",
};

//actions
export const setVisibleState = (data) => ({ type: actionTypes.visible, data });

//reducers
export const visibleReducer = (state = true, action) => {
  switch (action.type) {
    case actionTypes.visible:
      return action.data;
    default:
      return state;
  }
};

//get action from store in a connected form
export const ConnectedSearchArea = connect(null, { setVisibleState })(
  SearchArea
);
