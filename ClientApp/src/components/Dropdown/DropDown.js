import React, { useState } from 'react';
import "./DropDown.css";

export default function DropDown(props) {
    const [value, setValue] = useState('select choice');

    const onClick = (name) => {
        setValue(() => name)
    }

    return (
        <div className="trend-new pull-right">
            <button className="unstyled">
                <span>{value}</span>
                <i className="fa fa-caret-down"></i>
            </button>
            <div className="clickable-trend">
                <div><button type="button" onClick={() => onClick("Trending")} className="unstyled">Trending</button></div>
                <div><button type="button" onClick={() => onClick("New")} className="unstyled">New</button></div>
            </div>
        </div>

    )
}
