import React from 'react';
import VerticalCenter from '../vertical-center';
import './LoadingPlaceholder.css';

const LoadingPlaceholder = () => (
    <VerticalCenter>
        <div className="spinner-border text-warning loading-placeholder" role="status">
            <span className="sr-only"></span>
        </div>
        <div className="text-center text-warning">Loading</div>
    </VerticalCenter>
);

export default LoadingPlaceholder;
