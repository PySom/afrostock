import React from "react";
import "./_ResultArea.scss";

export default function ResentSearch({ name, onClick }) {
  return (
    <button onClick={onClick} type="button" className="b-recent mr-2 app-btn">
      <span className="mr-1 img-tag">{name}</span>
      <span className="fa fa-search img-tag"></span>
    </button>
  );
}
