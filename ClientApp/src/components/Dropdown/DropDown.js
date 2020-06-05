import React, { useState } from "react";
import "./DropDown.css";

export default function DropDown(props) {
  const [value, setValue] = useState("Default option");

  const sortByDate = (name) => {
    setValue(() => name);
    props.dateSort();
  };

  const sortByViews = (name) => {
    setValue(() => name);
    props.viewSort();
  };

  return (
    <div className="trend-new pull-right">
      <button className="unstyled">
        <span>{value}</span>
        <i className="fa fa-caret-down"></i>
      </button>
      <div className="clickable-trend">
        <div>
          <button
            type="button"
            onClick={() => sortByDate("Trending")}
            className="unstyled"
          >
            Trending
          </button>
        </div>
        <div>
          <button
            type="button"
            onClick={() => sortByViews("New")}
            className="unstyled"
          >
            New
          </button>
        </div>
      </div>
    </div>
  );
}
