import React, { useEffect } from 'react';
import './SearchBar.css';
import useFocus from '../../customHooks/useFocus';




export default function SearchBar({ className, searchValue, setShow, show, onChange, areaInView }) {
	const [inputRef, setInputFocus] = useFocus()
	useEffect(() => {
		if (!areaInView && show) {
			setInputFocus()
        }
	}, [areaInView, show, setInputFocus])
	return (
		<div className={`searchbox header-width ${className ? className : ""}`}>
			<input type="text" autoComplete="off" value={searchValue}
				ref={inputRef}
				onChange={({ target: { value } }) => onChange(value)}
				onFocus={() => setShow(true)} onBlur={() => setShow(areaInView ? true : false)}
				placeholder="Search..." name="search" className="search" />
			<button className="btn-search">
				<img src="https://img.icons8.com/cotton/24/000000/search--v2.png" alt="search icon"/>
			</button>
		</div>
    )
}



