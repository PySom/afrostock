import React, { useState, useEffect } from "react";
import file from "../../sideEffects/apis/file";
import api from "../../sideEffects/apis/api";
import { useLocation, useHistory, Link } from "react-router-dom";
import "./_EditProfile.scss";

export default function EditProfile(props) {
  return (
    <div className="container-fluid dashboard-wrapper">
      <div className="upload-section text-center">
        <form>
          <input type="file" onChange={uploadImage} />
        </form>
      </div>
    </div>
  );
}
