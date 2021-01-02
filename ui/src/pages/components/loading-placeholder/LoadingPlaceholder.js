import React from 'react';
import VerticalCenter from '../vertical-center';
import './LoadingPlaceholder.css';

const LoadingPlaceholder = ({ children, className = '' }) => (
    <VerticalCenter className={className}>
        <div className="spinner-border text-warning loading-placeholder" role="status">
            <span className="sr-only"></span>
        </div>
        <div className="text-center text-warning">{children}</div>
    </VerticalCenter>
);

export default LoadingPlaceholder;
