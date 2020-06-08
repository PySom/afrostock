import React from "react";
import "./_MiddleNav.scss";
import { NavLink } from "react-router-dom";
import { connect } from "react-redux";
import { getNavUrl } from "../../creators/trackNavCreator";

function MiddleNav(props) {
  const reloadTemp = (data) => {
    props.getNavUrl(data);
  };
  return (
    <div className="border-b">
      <nav className="middle-nav">
        <ul className="p-0">
          <li>
            <NavLink
              onClick={() => reloadTemp("/")}
              to="/"
              exact
              activeClassName="active-mid-nav"
            >
              Home
            </NavLink>
          </li>
          <li>
            <NavLink
              onClick={() => reloadTemp("/discover")}
              to="/discover"
              exact
              activeClassName="active-mid-nav"
            >
              Discover
            </NavLink>
          </li>
          <li>
            <NavLink
              onClick={() => reloadTemp("/videos")}
              to="/videos"
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

//get props value
const matchStateToProps = ({ navurl }) => {
  return { navurl };
};

export default connect(matchStateToProps, { getNavUrl })(MiddleNav);
