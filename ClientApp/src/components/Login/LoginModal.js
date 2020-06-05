import React from "react";
import auth from "../../sideEffects/apis/auth";
import { useForm } from "../../customHooks/useForm";
import { useLocation, useHistory, Link } from "react-router-dom";

export default function Modal(props) {
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
      })
      .catch((err) => {
        alert(err.message);
      });
  };

  return (
    <div>
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
          {props.type == "modal" && (
            <Link to={`/register${url && "?returnurl=" + url}`}>Register</Link>
          )}
        </div>
      </div>
    </div>
  );
}
