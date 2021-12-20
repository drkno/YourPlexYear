import React from 'react';
import VerticalCenter from '../vertical-center';

const Card = ({ children, colour, numx, numy, className = '', id = void(0) }) => {
    const xBreakpoint = Math.floor(12 / numx) + '';
    const yBreakpoint = Math.ceil(100 / numy) + '';
    const minHeight = `max(${yBreakpoint}vh, 500px)`;
    return (
        <div className={`col-12 col-lg-${xBreakpoint} ${className}`} style={{minHeight: minHeight, backgroundColor: colour }} id={id}>
            <VerticalCenter strategy='relative'>
                {children}
            </VerticalCenter>
        </div>
    );
};

export default Card;
