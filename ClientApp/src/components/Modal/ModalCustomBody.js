import React from 'react';
import "./Modal.css";

export default function ModalCustomBody({ src, tags }) {

    return (
        <div className="container-fluid">
            <div className="text-center">
                <img src={src} className="img-fluid" alt="full representation" />
            </div>
        </div>
    );
}
