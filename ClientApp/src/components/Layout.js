import React, { Component } from "react";
import { Container } from "reactstrap";
import { ConnectedNavMenu } from "./NavMenu";
import Loadar from "./Loader";
import { connect } from "react-redux";

function Layout(props) {
  // static displayName = Layout.name;

  return (
    <div className={props.loadar ? "d-none" : "d-block"}>
      <div>
        <ConnectedNavMenu />
        <Container fluid className="m-0 p-0">
          {props.children}
        </Container>
      </div>
    </div>
  );
}

const mapStateToProps = ({ loadar }) => {
  return { loadar };
};

export default connect(mapStateToProps, null)(Layout);
