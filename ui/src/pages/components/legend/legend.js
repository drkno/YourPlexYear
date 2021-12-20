import React, { Fragment } from 'react';
import { colourList, labelColour } from '../theme';

import './legend.css';

const LegendItem = ({ children, index }) => (
    <Fragment>
        <span className='chart-legend' style={{
            backgroundColor: colourList[index % colourList.length],
            color: labelColour[index % labelColour.length],
            padding: 3
        }}>{children}</span>&nbsp;
    </Fragment>
);

const Legend = ({ legendData }) => (
    <div className='chart-legend-container'>
        {legendData.map((data, i) => (<LegendItem key={'legend-' + i} index={i}>{data.title}</LegendItem>))}
    </div>
);

export default Legend;
