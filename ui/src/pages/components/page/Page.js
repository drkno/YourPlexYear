import React from 'react';
import './Page.css';

const Page = ({ children, className='bg-white', anchor = void(0) }) => {
    return (
        <div id={anchor} className={`container-fluid page-container text-center ${className}`}>
            {children}
        </div>
    );
};

export default Page;
