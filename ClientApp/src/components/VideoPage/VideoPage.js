import React, { useState, useEffect } from 'react';
import PhotoGrid from '../PhotoGrid/PhotoGrid';
import api from '../../sideEffects/apis/api';

export default function VideoPage(props) {
    const [pageLoaded, setPageLoaded] = useState(true);
    const [contents, setContents] = useState([]);

    useEffect(() => {
        if (pageLoaded) {
            let url = null;
            if (props.id) {
                url = `images/videos/searchfor/?term=${props.id}`;
            }
            else {
                url = "images/videos";
            }
            api.getAll(url)
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
        <div>
            <PhotoGrid contents={contents} />
        </div>
    )
}