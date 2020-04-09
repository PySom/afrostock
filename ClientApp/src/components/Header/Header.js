import React from 'react';
import SearchBar from '../SearchBar/SearchBar';

export default function Header() {
    return (
        <div className="">
            <div className="m0-auto header-width">
                <h1 className="hero__title text-white">The best free stock photos shared by talented creators.</h1>
            </div>
            <SearchBar />
        </div>
    )
}
