import React, { Component } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import "./_NavMenu.scss";
import history from "./history";
import { SearchArea } from "./SearchArea/SearchArea";
import { connect } from "react-redux";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }

  render() {
    const { searchVisibility } = this.props;
    console.log("nav visible", searchVisibility);
    return (
      <header
        className={`sticky-header ${searchVisibility ? "" : "head-w-search"} ${
          history.location.pathname == "/register" ||
          history.location.pathname == "/login"
            ? "d-none"
            : ""
        }`}
      >
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white"
          light
        >
          <Container fluid>
            <NavbarBrand href="/">
              <img src="images/logo.png" alt="logo" />
            </NavbarBrand>
            <SearchArea
              searchClass=" header-width nav-search"
              className={`variable-search-width ${
                searchVisibility ? "d-none" : ""
              }`}
            />
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse
              className="d-sm-inline-flex flex-sm-row-reverse"
              isOpen={!this.state.collapsed}
              navbar
            >
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink href="/" className="text-dark" to="/">
                    Home
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="/aboutUs" className="text-dark">
                    About Us
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="/contact" className="text-dark">
                    Contact
                  </NavLink>
                </NavItem>
                <NavItem className="register">
                  <NavLink href="/register" className="text-dark">
                    Join
                  </NavLink>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

//get props value
const matchStateToProps = ({ searchVisibility }) => {
  return { searchVisibility };
};

//connect layout to get search visibility
export const ConnectedNavMenu = connect(matchStateToProps)(NavMenu);
