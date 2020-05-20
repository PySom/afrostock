import React, { useState, useEffect } from "react";
import file from "../../sideEffects/apis/file";
import api from "../../sideEffects/apis/api";
import { useLocation, useHistory, Link } from "react-router-dom";
import "./_Dashboard.scss";

export default function Upload(props) {
  const [fileUploadResponse, setFileUploadResponse] = useState(null);
  const [imageResponse, setImageResponse] = useState(null);
  const uploadImage = async (e) => {
    const newFile = e.target.files[0];
    let fileObj = new FormData();
    fileObj.append("file", newFile);
    const fileResponse = await file.addFile(fileObj);
    let imageData = {
      name: fileResponse.name,
      description: "Test the image ",
      contentType: 0,
      content: fileResponse.content,
      contentLow: fileResponse.contentLow,
      authorId: 2,
      amount: 0,
      suggestedTags: fileResponse.suggestedTags,
    };

    const imageResponseData = await api.create(
      "images",
      imageData,
      imageData.name
    );
    setFileUploadResponse(fileResponse);
    setImageResponse(imageResponseData);
  };

  useEffect(() => {
    console.log(fileUploadResponse);
    if (imageResponse) {
      alert("upload successful, go to homepage to see image");
    }
  });
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
