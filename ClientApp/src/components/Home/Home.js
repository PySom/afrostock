import React from 'react';
import Header from '../Header/Header';
import MiddleNav from '../MiddleNav/MiddleNav';
import DropDown from '../Dropdown/DropDown';
import PhotoGrid from '../PhotoGrid/PhotoGrid';

export default function Home(props) {
    return (
        <>
            <div className="header-bg pt-1h pb-1h plr-15">
                <Header />
            </div>
            <MiddleNav />
            <div className="enforce-mp trend-pos mt-20">
                <p className="app-font-mid">Free Stock Photos</p>
                <DropDown />
            </div>
            <div className="enforce-mp mt-20">
                <PhotoGrid />
            </div>
        </>
    )
}
