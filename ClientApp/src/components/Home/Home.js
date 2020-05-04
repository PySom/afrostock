import React, { useState, useEffect } from 'react';
import Header from '../Header/Header';
import MiddleNav from '../MiddleNav/MiddleNav';
import DropDown from '../Dropdown/DropDown';
import PhotoGrid from '../PhotoGrid/PhotoGrid';
import api from '../../sideEffects/apis/api';

export default function Home(props) {
    const [contents, setContents] = useState([]);
    const [pageLoaded, setPageLoaded] = useState(true);

    useEffect(() => {
        if (pageLoaded) {
            api.getAll("images")
                .then(response => {
                    setContents(response)
                    setPageLoaded(false)
                })
                .catch(err => {
                    console.log(err)
                    setPageLoaded(false)
                })
        }
    }, [pageLoaded])

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
                <PhotoGrid contents={contents} />
            </div>
        </>
    )
}
