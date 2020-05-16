import React from "react";
import auth from "../../sideEffects/apis/auth";
import { useForm } from "../../customHooks/useForm";
import { useLocation, useHistory, Link } from "react-router-dom";

export default function Login(props) {
  const location = useLocation();
  console.log({ location }, props.route);
  const history = useHistory();
  const url =
    location && location.search
      ? location.search.substring(location.search.indexOf("=") + 1)
      : "";
  const { main: email } = useForm("email", "");
  const { main: password } = useForm("password", "");
  const submitForm = () => {
    const data = { email: email.value, password: password.value };
    auth
      .login(data)
      .then((res) => {
        if (props.route) {
          history.push(props.route);
        } else {
          history.push((url && "/" + url) || "/");
        }
      })
      .catch((err) => {
        alert(err.message);
      });
  };

  return (
    <div>
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

      <div className="register-wrapper">
        <div className="form-wrapper">
          <form onSubmit={submitForm}>
            <div>
              <input {...email} placeholder="email" />
            </div>
            <div>
              <input {...password} placeholder="password" />
            </div>
            <div>
              <button type="submit">Login</button>
            </div>
          </form>
          {props.type !== "modal" && (
            <Link to={`/register${url && "returnurl=" + url}`}>Register</Link>
          )}
        </div>
      </div>
    </div>
  );
}
