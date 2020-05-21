import React, { useState, useEffect } from "react";
import file from "../../sideEffects/apis/file";
import api from "../../sideEffects/apis/api";
import { useLocation, useHistory, Link } from "react-router-dom";
import { useFormFile } from "../../customHooks/useForm";
import { useForm } from "../../customHooks/useForm";
import "./_EditProfile.scss";

export default function EditProfile(props) {
  const [avatar, setAvatar] = useState("avatar.png");
  const { main: email } = useForm("email", "");
  const { main: firstName } = useForm("text", "");
  const { main: LastName } = useForm("text", "");
  const { main: bio } = useForm("text", "");
  const { main: instagram } = useForm("text", "");
  const { main: twitter } = useForm("text", "");
  const { main: location } = useForm("text", "");
  const { main: password } = useForm("password", "");

  return (
    <div className="container-fluid editprofile-wrapper">
      <div className="edit-section">
        <div className="form-wrapper">
          <div className="heading">
            <h3>Edit Your Profile</h3>
          </div>

          <form>
            <div className="picture-section row">
              <div className="col-4">
                <img
                  className="img-fluid"
                  src={`images/${avatar}`}
                  alt="uploaded profile image"
                />
              </div>
              <div className="col-3">
                <input type="file" />
              </div>
            </div>
            <div className="name-section">
              <div className="first-name">
                <label>First Name</label>
                <input {...firstName} />
              </div>
              <div className="last-name">
                <label>Last Name</label>
                <input {...LastName} />
              </div>
            </div>
            <div className="full-section">
              <label>Email</label>
              <input {...email} />
            </div>
            <div className="full-section">
              <label>Short Bio</label>
              <input {...bio} />
            </div>
            <div className="full-section">
              <label>Instagram</label>
              <input {...instagram} />
            </div>
            <div className="full-section">
              <label>Twitter</label>
              <input {...twitter} />
            </div>
            <div className="full-section">
              <label>Location</label>
              <input {...location} />
            </div>
            <div className="full-section">
              <span>Password</span>
              <div className="change-password">
                <Link to="/changepassword">Change Password</Link>
              </div>
            </div>
            <div className="submit">
              <button type="submit">Update</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
