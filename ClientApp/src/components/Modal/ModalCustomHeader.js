import React from 'react';
import "./Modal.css";

export default function ModalCustomHeader({ authorName, views }) {

    return (
        <div className="app-flex">
            <p>{authorName}</p>
            <div className="app-flex pull-right">
                <div className="mr-4">
                    <button>
                        <span className="fa fa-eye mr-1"></span>
                        {views}
                    </button>
                </div>
                <div>
                    <button>Download</button>
                </div>
                
            </div>
        </div>
        );
}
