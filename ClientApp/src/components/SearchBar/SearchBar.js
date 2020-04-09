import React from 'react';
import './SearchBar.css';

export default function SearchBar({ className }) {
	return (
		<div className={`searchbox header-width ${className}`}>
			<input id="search" type="text" placeholder="Search..." name="search" className="search" />
			<button className="btn-search">
				<img src="https://img.icons8.com/cotton/24/000000/search--v2.png" alt="search icon"/>
			</button>
		</div>
    )
}
