import React from 'react';
import "./MiddleNav.css"
import { NavLink } from 'react-router-dom';

export default function MiddleNav(props) {
    return (
        <div className="border-b">
            <nav className="middle-nav">
                <ul className="p-0">
                    <li><NavLink to='/' exact activeClassName="active-mid-nav">Home</NavLink></li>
                    <li><NavLink to='/discover' exact activeClassName="active-mid-nav">Discover</NavLink></li>
                </ul>
            </nav>
        </div>
    )
}
