import React from 'react';
import MiddleNav from '../MiddleNav/MiddleNav';

export default function MainBody({ children }) {
    return (
        <div>
            <MiddleNav />
            {children}
        </div>
    )
}