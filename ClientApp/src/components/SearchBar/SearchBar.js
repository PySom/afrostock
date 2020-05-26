import React, { useEffect } from "react";
import "./_SearchBar.scss";
import useFocus from "../../customHooks/useFocus";

export default function SearchBar({
  className,
  searchValue,
  setShow,
  show,
  onChange,
  areaInView,
  onKeyPress,
}) {
  const [inputRef, setInputFocus] = useFocus();
  useEffect(() => {
    if (!areaInView && show) {
      setInputFocus();
    }
  }, [areaInView, show, setInputFocus]);
  return (
    <div className={`searchbox ${className ? className : ""}`}>
      <input
        type="text"
        autoComplete="off"
        value={searchValue}
        ref={inputRef}
        onChange={({ target: { value } }) => onChange(value)}
        onKeyPress={(event) => {
          if (event.key === "Enter") {
            onKeyPress();
          }
        }}
        onFocus={() => setShow(true)}
        onBlur={() => setShow(areaInView ? true : false)}
        placeholder="Search..."
        name="search"
        className="search"
      />
      <button className="btn-search">
        <img
          src="https://img.icons8.com/cotton/24/000000/search--v2.png"
          alt="search icon"
        />
      </button>
    </div>
  );
}
