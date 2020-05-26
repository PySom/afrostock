import React, { Component, useState, useEffect } from "react";
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
import { setLoggedInStatus } from "./Register/Register";
import { connect } from "react-redux";

import {
  ButtonDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from "reactstrap";
import { logout } from "../sideEffects/apis/auth";

const ProfileDropDown = (props) => {
  const [dropdownOpen, setOpen] = useState(false);

  const toggle = () => setOpen(!dropdownOpen);
  const triggerLogout = () => {
    props.logout(false);
    setLoggedInStatus(false);
    history.push("/");
    window.location.reload();
  };

  return (
    <ButtonDropdown
      isOpen={dropdownOpen}
      toggle={toggle}
      className="profile_button"
    >
      <DropdownToggle caret className="wrap__avatar">
        <img
          class="img-fluid nav__avatar"
          src="images/avatar.png"
          alt="profile avatar"
        />
      </DropdownToggle>
      <DropdownMenu>
        <NavLink href="/dashboard" className="text-dark px-0">
          <DropdownItem>Your Profile</DropdownItem>
        </NavLink>

        <DropdownItem>Your Collection</DropdownItem>
        <DropdownItem onClick={triggerLogout}>Logout</DropdownItem>
        <DropdownItem divider />
        <DropdownItem>FAQ</DropdownItem>
      </DropdownMenu>
    </ButtonDropdown>
  );
};

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      loggedIn: false,
      showSearch: false,
    };
  }

  componentDidMount() {
    if (localStorage.getItem("user")) {
      this.setState({ loggedIn: true });
    }
  }

  logout(value) {
    localStorage.removeItem("user");
    this.setState({ loggedIn: value });
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
      showSearch: !this.state.showSearch,
    });
  }

  render() {
    const { searchVisibility } = this.props;
    console.log("nav visible", searchVisibility);
    return (
      <header
        className={`sticky-header ${searchVisibility ? "" : "head-w-search"} ${
          this.props.loggedInStatus ? "d-none" : ""
        } ${
          history.location.pathname == "/" ||
          history.location.pathname == "/register" ||
          history.location.pathname == "/login"
            ? ""
            : "color_nav"
        }`}
      >
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white"
          light
        >
          <Container fluid>
            <NavbarBrand href="/">
              <img
                src={searchVisibility ? "images/logo.png" : "images/emblem.png"}
                alt="logo"
              />
            </NavbarBrand>
            <SearchArea
              searchClass=" header-width nav-search"
              className={`variable-search-width ${
                searchVisibility || (searchVisibility && !this.state.showSearch)
                  ? "d-none"
                  : ""
              }`}
            />
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse
              className="d-sm-inline-flex flex-sm-row-reverse "
              isOpen={!this.state.collapsed}
              navbar
              // style={{ height: "100vh" }}
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

                {this.state.loggedIn ? (
                  <>
                    <NavItem className="d__none">
                      <ProfileDropDown logout={logout} />
                    </NavItem>
                    <NavItem className="register d__none">
                      <NavLink href="/upload" className="text-dark">
                        Upload
                      </NavLink>
                    </NavItem>
                    <NavItem className="d--one">
                      <NavLink href="/upload" className="text-dark">
                        Upload
                      </NavLink>
                    </NavItem>
                  </>
                ) : (
                  <NavItem className="register">
                    <NavLink href="/register" className="text-dark">
                      Join
                    </NavLink>
                  </NavItem>
                )}
                <NavItem>
                  <NavLink href="/contact" className="text-dark">
                    Contact
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
const matchStateToProps = ({ searchVisibility, loggedInStatus }) => {
  return { searchVisibility, loggedInStatus };
};

//connect layout to get search visibility
export const ConnectedNavMenu = connect(matchStateToProps, {
  setLoggedInStatus,
})(NavMenu);
