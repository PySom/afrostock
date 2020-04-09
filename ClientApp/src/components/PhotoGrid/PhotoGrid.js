import React from 'react';
import "./PhotoGrid.css";
import getImages from '../../helper/imageHelper';

export default function PhotoGrid(props) {
    const images = getImages(25);
    return (
        <section id="photos">
            {images.map((src, index) => (<img src={src} alt="awesome cats" key={index} />))}
        </section>
        )
}
