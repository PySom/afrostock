import React from 'react';
import { ConnectedSearchArea } from '../SearchArea/SearchArea';

export default function Header() {
    return (
        <div className="">
            <div className="m0-auto header-width header-search">
                <h1 className="hero__title text-white">The best free stock photos shared by talented creators.</h1>

            </div>
            <ConnectedSearchArea type="head" searchClass="header-search header-width" />
            
        </div>
    )
}
