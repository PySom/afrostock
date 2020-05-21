import React, { useState, useEffect } from "react";
import auth from "../../sideEffects/apis/auth";
import { useForm } from "../../customHooks/useForm";
import { connect } from "react-redux";
import { useLocation, useHistory, Link } from "react-router-dom";
import "./_Dashboard.scss";

function Dashboard(props) {
  const [showLoggedin, setShowLoggedIn] = useState(false);
  const [loggedInUser, setLoggedInUser] = useState(null);

  const history = useHistory();
  useEffect(() => {
    setLoggedInUser(JSON.parse(localStorage.getItem("user")));
    setShowLoggedIn(true);
    hideLoggedInDisplay();
  }, []);

  const hideLoggedInDisplay = () => {
    console.log("user is", loggedInUser);
    setTimeout(() => {
      setShowLoggedIn(false);
    }, 50000);
  };

  const goToUploadPage = () => {
    history.push("/upload");
  };

  const goToEditProfile = () => {
    history.push("/editprofile");
  };
  return (
    <div className="dashboard-wrapper">
      <div
        className={`show-loggedIn text-center ${
          showLoggedin ? "d-block" : "d-none"
        }`}
      >
        <h6>You are logged in</h6>
      </div>

      {loggedInUser && (
        <div className="container-fluid ">
          <div className="profile-summary text-center">
            <div className="bio-wrapper row">
              <div className="col-md-3">
                <div className="avartar">
                  <img
                    className="img-fluid"
                    src="images/avatar.png"
                    alt="profile avatar"
                  />
                </div>
              </div>

              <div className="col-md-9">
                <div className="profile-information">
                  <div className="user_names row">
                    <div className="col-md-8">
                      <span>{`${loggedInUser.firstName} ${loggedInUser.surName}`}</span>
                    </div>
                    <div className="col-md-4 ">
                      <button onClick={goToEditProfile}>
                        <img
                          className="img-fluid"
                          src="images/pencilEdit.png"
                        />
                        Edit profile
                      </button>
                    </div>
                  </div>
                  <div className="profile-extras">
                    <div className="col--3">
                      <img src="images/pin-8-xxl.png" alt="location pin" />
                      <span>Lagos, Nigeria</span>
                    </div>
                    <div className="col--3">
                      <img src="images/instagram-xxl.png" alt="location pin" />
                      <span>emekaachugo</span>
                    </div>
                    <div className="col--3">
                      <img src="images/twitter-xxl.png" alt="location pin" />
                      <span>_achugo</span>
                    </div>
                  </div>
                  <div className="bio-summary">
                    <span>
                      Father, husband, Lover of Good photograph and dark scenery
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="image-upload text-center">
            <div className="cloud-upload">
              <img src="images/cloud-upload-xxl.png" />
            </div>
            <div className="upload-captions">
              <h4>Start Uploading Your Photos to AfroStock</h4>
              <h5>
                Do you have outstanding photos that you want to share with the
                Pexels community?
              </h5>
            </div>
            <div className="upload-link">
              <button onClick={goToUploadPage}>Upload Your Photos</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

const matchStateToProps = ({ loggedInStatus }) => {
  return { loggedInStatus };
};

//connect layout to get search visibility
export default connect(matchStateToProps)(Dashboard);
