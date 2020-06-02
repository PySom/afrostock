import React from "react";
import "./_MiddleNav.scss";
import { NavLink } from "react-router-dom";

export default function MiddleNav(props) {
  const reloadTemp = (data) => {
    setTimeout(() => {
      window.location.reload();
    }, 10);
  };
  return (
    <div className="border-b">
      <nav className="middle-nav">
        <ul className="p-0">
          <li>
            <NavLink
              to="/"
              onClick={reloadTemp}
              exact
              activeClassName="active-mid-nav"
            >
              Home
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/discover"
              onClick={reloadTemp}
              exact
              activeClassName="active-mid-nav"
            >
              Discover
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/videos"
              onClick={reloadTemp}
              exact
              activeClassName="active-mid-nav"
            >
              Video
            </NavLink>
          </li>
        </ul>
      </nav>
    </div>
  );
}
