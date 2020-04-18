import React from 'react';
import './ResultArea.css';

export default function ResentSearch({ name }) {
	return (
        <button className="b-recent mr-2 app-btn">
            <span className="mr-1 img-tag">{name}</span>
            <span className="fa fa-search img-tag"></span>
        </button>
	)
}
