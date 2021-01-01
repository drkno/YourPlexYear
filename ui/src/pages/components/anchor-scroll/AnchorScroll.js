import React, { useState } from 'react';
import './AnchorScroll.css';

const AnchorScroll = ({ anchor, direction = 'down' }) => {
    const [hover, setHover] = useState(false);
    return (
        <div className={`anchor-scroll anchor-scroll-${direction}`}
            onMouseOver={() => setHover(true)}>
            <a href={'#' + anchor}>❯</a>
        </div>
    );
};

export default AnchorScroll;
