import React from 'react';
import './ResultArea.css';
import { Link } from 'react-router-dom';

export default function TrendingArea({ src, name, id }) {
    return (
        <div className="app-btn s-trending-area">
            <Link to={`/${id}`} className="unstyled-a mr-2">
                <img src={src} className="img-circle f-s-50 mr-2" alt={name} />
                <span className="img-tag">{name}</span>
            </Link>
        </div>
    )
}
