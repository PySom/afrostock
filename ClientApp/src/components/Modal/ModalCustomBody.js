import React from 'react';
import "./Modal.css";

export default function ModalCustomBody({ src, contentType, name, description, tags }) {
    console.log({ src, contentType, name, description })
    return (
        <div className="container-fluid">
            <div className="text-center">
                {
                    contentType === 0 &&
                    <figure className="r-p">
                        <img src={src} alt={name} title={description || name} className="img-fluid" />
                    </figure>
                }

                {
                    contentType === 1 &&
                    <video controls src={src}>
                    </video>
                }
            </div>
        </div>
    );
}
