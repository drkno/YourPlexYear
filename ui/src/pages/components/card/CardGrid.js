import React from 'react';
import './CardGrid.css';

const CardGrid = ({ children }) => (
    <div className='row h3 card-grid'>
        {children}
    </div>
);

export default CardGrid;
