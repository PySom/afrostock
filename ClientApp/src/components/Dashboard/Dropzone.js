import React, { useState, useEffect } from "react";
import { useDropzone } from "react-dropzone";
import file from "../../sideEffects/apis/file";
import api from "../../sideEffects/apis/api";
import { setLoader } from "../../creators/loaderCreator";
import { connect } from "react-redux";
import "./_Dropzone.scss";

function Dropzone(props) {
  //   const [uploadedFiles, setUploadedFiles] = useState([]);
  const [fields, setFields] = useState([]);
  const [textareaFields, setTextAreaFields] = useState([]);
  const [alluploadedFiles, setAllUploadedFiles] = useState(null);
  const [responseSuccess, setResponseSuccess] = useState(false);
  const { acceptedFiles, getRootProps, getInputProps } = useDropzone();
  const [showPublishButton, setShowPublishButton] = useState(false);

  //   const files = acceptedFiles.map((file) => (
  //     <li key={file.path}>
  //       {file.path} - {file.size} bytes
  //     </li>
  //   ));
  useEffect(() => {
    props.setLoader(false);
  }, []);

  const removeFromUpload = (item) => {
    const records = [...alluploadedFiles];
    records.splice(item, 1);
    setAllUploadedFiles(records);
  };

  const files = alluploadedFiles ? (
    alluploadedFiles.map((file, index) => (
      <React.Fragment key={file.name}>
        <div className="row solo__upload">
          <div className="col-md-6">
            {file.contentType == "image" ? (
              <img
                className="img-fluid"
                src={file.contentLower}
                alt="uploaded image"
              />
            ) : (
              <video width="400" height="300" controls>
                <source src={file.contentLow} type="video/mp4"></source>
              </video>
            )}
          </div>
          <div className="col-md-6">
            <div className="description__field">
              <textarea
                type="text"
                placeholder="Enter description"
                onChange={(event) => onChangeTextAreaInput(index, event)}
              />
            </div>
            <div className="amount__field">
              <input
                type="text"
                placeholder="Set Amount"
                onChange={(event) => onChangeFormGroupInput(index, event)}
              />
            </div>
            <div className="button__section">
              <button type="button" onClick={() => removeFromUpload(index)}>
                Remove
              </button>
            </div>
          </div>
        </div>
      </React.Fragment>
    ))
  ) : (
    <React.Fragment>
      <img className="img-fluid" src="images/loader.gif"></img>
    </React.Fragment>
  );

  console.log(fields);
  const showfiles = (e) => {
    console.log(e.target.value);
  };

  const onChangeFormGroupInput = (index, event) => {
    let fields_ = [...fields];
    fields_[index] = event.target.value;
    console.log(event.target.value);
    setFields(fields_);
  };

  const onChangeTextAreaInput = (index, event) => {
    let fields_ = [...textareaFields];
    fields_[index] = event.target.value;
    console.log(event.target.value);
    setTextAreaFields(fields_);
  };
  //   const asyncFuncforFiles = async (payload) => {
  //     let fileObj = new FormData();
  //     fileObj.append("file", payload);
  //     console.log(fileObj);
  //     const fileResponse = await file.addFile(payload);

  //     return fileResponse;
  //   };
  //   const uploadFiles = async (e) => {
  //     const newFile = e.target.files;
  //     const getArray = Object.keys(newFile).map((key) => newFile[key]);
  //     console.log(getArray);
  //     return Promise.all(
  //       getArray.forEach((data) => {
  //         asyncFuncforFiles(data);
  //       })
  //     );
  //   };

  //   const allFiles = async (data) => {
  //     const waitingResponse = await uploadFiles(data);
  //     //setUploadedFiles(waitingResponse);
  //     console.log(waitingResponse);
  //   };

  const allFiles = (e) => {
    const filez = e.target.files;
    let length_ = e.target.files.length;
    let arrayLen = [];
    let inputFields = [];
    const resp_ = Array.from(filez).forEach((element, index) => {
      let formData = new FormData();
      formData.append("file", element);
      file.addFile(formData).then((res) => {
        console.log(res);
        arrayLen = arrayLen.concat(res);
        inputFields = inputFields.concat("");
        if (index == length_ - 1) {
          setShowPublishButton(true);
          setAllUploadedFiles(arrayLen);
          setFields(inputFields);
          setTextAreaFields(inputFields);
          alert(arrayLen.length + " files uploaded");
        }
      });
    });
    setResponseSuccess(true);
  };

  const publishAllFiles = (e) => {
    e.preventDefault();
    const resp_ = alluploadedFiles.forEach((element, index) => {
      let data = {
        ...element,
        description: textareaFields[index],
        amount: Number(fields[index]),
        contentType: element.contentType == "image" ? 0 : 1,
        authorId: 1,
        suggestedTags: element.suggestedTags,
      };
      api.create("images", data, data.name).then((res) => console.log(res));
      if (index == alluploadedFiles.length - 1) {
        alert(alluploadedFiles.length + " files published");
      }
    });
    //console.log(resp_);
  };

  return (
    <div className="">
      <div className="uploads">
        <section className="drag__and__drop">
          <div {...getRootProps({ className: "dropzone" })}>
            <input {...getInputProps()} onChange={(event) => allFiles(event)} />
            <p>
              <button type="button">Browse</button>
              <span>or Drag 'n' drop some files here</span>{" "}
            </p>
          </div>
        </section>
        <div className="upload__data">{responseSuccess && files}</div>
      </div>
      <div className="publish__upload">
        <form onSubmit={publishAllFiles}>
          <input type="hidden" />
          <button
            className={showPublishButton ? "d-inline" : "d-none"}
            type="submit"
          >
            PUBLISH
          </button>
        </form>
      </div>
    </div>
  );
}

export default connect(null, { setLoader })(Dropzone);
