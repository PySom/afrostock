import React, { useEffect } from "react";
import auth from "../../sideEffects/apis/auth";
import { useForm } from "../../customHooks/useForm";
import { connect } from "react-redux";
import { useLocation, useHistory, Link } from "react-router-dom";
import { setLoggedInStatus } from "../Register/Register";

function Login(props) {
  useEffect(() => {
    props.setLoggedInStatus(true);
  }, []);

  useEffect(() => {
    return () => {
      props.setLoggedInStatus(false);
    };
  }, []);

  const location = useLocation();
  console.log({ location }, props.route);
  const history = useHistory();
  const url =
    location && location.search
      ? location.search.substring(location.search.indexOf("=") + 1)
      : "";
  console.log({ url });

  const { main: email } = useForm("email", "");
  const { main: password } = useForm("password", "");
  const submitForm = (e) => {
    e.preventDefault();
    const data = { email: email.value, password: password.value };
    auth
      .login(data)
      .then((res) => {
        if (typeof props.callback === "function") props.callback();

        if (props.route) {
          history.push(props.route);
        } else {
          if (typeof props.closeMe === "function") props.closeMe();
          else if (url) history.push("/" + url);
          else history.push("/dashboard");
        }
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
    <div>
      <div className="auth-overlay">
        <div className="heading">
          <div className="row">
            <div className="logo-section">
              <img src="images/logo.png" alt="logo" onClick={goToHome} />
            </div>
            <div className="caption">
              <span>New to AfroStock?</span>
              {props.type !== "modal" && <Link to="/register">Register</Link>}
            </div>
          </div>
        </div>
        <div className="register-wrapper">
          <div className="form-wrapper">
            <div className="heading">
              <h3>Welcome Back to AfroStock</h3>
            </div>
            <form onSubmit={submitForm}>
              <div className="full">
                <input {...email} placeholder="email" />
              </div>
              <div className="full">
                <input {...password} placeholder="password" />
              </div>
              <div className="submit_">
                <button type="submit">Login</button>
              </div>
            </form>
            {props.type !== "modal" && (
              <Link to={`/register${url && "?returnurl=" + url}`}>
                Register
              </Link>
            )}
          </div>
        </div>
      </div>

      <div className="auth-images">
        <div className="auth-images-wrap">
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

export default connect(null, { setLoggedInStatus })(Login);
