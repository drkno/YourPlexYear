import React from 'react';
import './VerticalCenter.css';

const VerticalCenter = ({ children, className = '', strategy = 'absolute' }) => (
    <div className={`vertical-center-container-${strategy} ${className}`}>
        <div className='vertical-center'>
            {children}
        </div>
    </div>
);

export default VerticalCenter;
