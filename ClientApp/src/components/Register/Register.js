import React from 'react';
import auth from '../../sideEffects/apis/auth';
import { useForm } from '../../customHooks/useForm';
import { useLocation, useHistory, Link } from 'react-router-dom';

export default function Register(props) {
    const location = useLocation();
    const history = useHistory();
    const url = location && location.search ? location.search.substring(location.search.indexOf('=') + 1) : "";
    const { main: email } = useForm("email");
    const { main: password } = useForm("password");
    const submitForm = () => {
        const data = { email: email.value, password: password.value }
        auth.register(data)
            .then(res => {
                history.push(url && '/' + url || '/');
            })
            .catch(err => {
                alert(err.message);
            })
    }

    return (
        <div>
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
            {props.type !== "modal" && <Link to={`/login${url && "returnurl=" + url}`}>Login</Link>}
        </div>
    )
}