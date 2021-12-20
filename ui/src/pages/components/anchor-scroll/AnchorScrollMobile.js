import React, { useState } from 'react';
import './AnchorScroll.css';

const AnchorScrollMobile = ({ anchor, direction = 'down' }) => {
    const [hover, setHover] = useState(false);
    return (
        <div className={`anchor-scroll anchor-scroll-${direction} anchor-scroll-mobile`}
            onMouseOver={() => setHover(true)}>
            <a href={'#' + anchor}>❯</a>
        </div>
    );
};

export default AnchorScrollMobile;
