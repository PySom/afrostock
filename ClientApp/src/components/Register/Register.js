import React from "react";
import auth from "../../sideEffects/apis/auth";
import { useForm } from "../../customHooks/useForm";
import { connect } from "react-redux";
import { useLocation, useHistory, Link } from "react-router-dom";
import "./_Register.scss";

function Register(props) {
  const location = useLocation();
  const history = useHistory();
  const url =
    location && location.search
      ? location.search.substring(location.search.indexOf("=") + 1)
      : "";
  const { main: email } = useForm("email", "");
  const { main: password } = useForm("password", "");
  const { main: firstName } = useForm("text", "");
  const { main: surName } = useForm("text", "");
  const { main: phone } = useForm("text", "");
  const { main: confirm } = useForm("password", "");
  const submitForm = (e) => {
    e.preventDefault();
    const data = {
      email: email.value,
      firstName: firstName.value,
      surName: surName.value,
      phoneNumber: phone.value,
      password: password.value,
      confirmPassword: confirm.value,
      sex: "Male",
      role: "Super",
    };
    auth
      .register(data)
      .then((res) => {
        history.push((url && "/" + url) || "/dashboard");
        window.location.reload();
      })
      .catch((err) => {
        alert(err.message);
      });
  };

  const goToHome = () => {
    history.push("/");
  };

  return (
    <div style={{ overflowY: "hidden" }}>
      <div className="auth-overlay">
        <div className="heading">
          <div className="row">
            <div className="logo-section">
              <img src="images/logo.png" alt="logo" onClick={goToHome} />
            </div>
            <div className="caption">
              <span>Already a member?</span>
              {props.type !== "modal" && (
                <Link to={`/login${url && "?returnurl=" + url}`}>Login</Link>
              )}
            </div>
          </div>
        </div>
        <div className="register-wrapper">
          <div className="form-wrapper">
            <div className="heading">
              <h2>Join AfroStock</h2>
              <span>
                Discover thousands of free photos you can use everywhere.
              </span>
            </div>
            <form onSubmit={submitForm}>
              <div className="surname-reg">
                <input {...surName} placeholder="Enter Last name" />
              </div>
              <div className="firstname-reg">
                <input {...firstName} placeholder="Enter first name" />
              </div>
              <div className="full">
                <input {...email} placeholder="email" />
              </div>
              <div className="full">
                <input {...phone} placeholder="Phone Number" />
              </div>
              <div className="full">
                <input {...password} placeholder="password" />
              </div>
              <div className="full">
                <input {...confirm} placeholder="confirm password" />
              </div>
              <div className="submit_">
                <button type="submit">SIGN UP</button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div className="auth-images">
        <div className="auth-images-wrap">
          <div className="wrap__img">
            <img className="img-fluid" src="images/stack1.jpg" alt="img" />
          </div>
          <div className="wrap-img">
            <div className="img_column">
              <div className="single_img">
                <img className="img-fluid" src="images/stack1.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack8.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack4.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack3.jpg" alt="img" />
              </div>
            </div>
            <div className="img_column">
              <div className="single_img">
                <img className="img-fluid" src="images/stack6.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack4.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack2.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack5.jpg" alt="img" />
              </div>
            </div>
            <div className="img_column">
              <div className="single_img">
                <img className="img-fluid" src="images/stack9.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack1.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack7.jpg" alt="img" />
              </div>
              <div className="single_img">
                <img className="img-fluid" src="images/stack6.jpg" alt="img" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

//Below defines actions and reducers
const actionTypes = {
  loggedInStatus: "LOGIN_STATUS",
};

//actions
export const setLoggedInStatus = (data) => ({
  type: actionTypes.loggedInStatus,
  data,
});

//reducers
export const loggedInStatusReducer = (state = false, action) => {
  switch (action.type) {
    case actionTypes.loggedInStatus:
      return action.data;
    default:
      return state;
  }
};

//get action from store in a connected form
export default connect(null, { setLoggedInStatus })(Register);
