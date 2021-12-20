import React, { Fragment } from 'react';
import { ArcSeries, XYPlot, LabelSeries } from 'react-vis';
import Legend from '../legend';
import { colourList, labelColour } from '../theme';

import './radial-chart.css';

const RadialChart = ({ data, dataKey = 'dataKey', titleKey = 'titleKey' }) => {
    const arcLength = (2 * Math.PI) / data.length;
    const formattedData = data.map((entry, i) => ({
        radius0: 0,
        radius: entry[dataKey],
        angle0: arcLength * i,
        angle: arcLength * (i + 1),
        color: colourList[i % colourList.length]
    }));

    const total = data.reduce((acc, next) => acc + next[dataKey], 0);
    const formattedLabels = data.map((entry, i) => ({
        radius0: 0,
        radius: entry[dataKey] * 0.8,
        angle0: arcLength * -((i + 4.75) % data.length),
        angle: arcLength * -((i + 4.75) % data.length + 1),
        label: parseFloat((entry[dataKey] * 100 / total).toPrecision(2)) + '%',
        style: {
            fill: labelColour[i % labelColour.length]
        }
    }));

    const legendData = data.map((entry, i) => ({
        title: entry[titleKey],
        color: colourList[i % colourList.length]
    }));

    return (
        <Fragment>
            <XYPlot
                className='radial-chart'
                width={400}
                height={400}>
                <ArcSeries
                    data={formattedData}
                    colorType={'literal'} />
                <LabelSeries
                    data={formattedLabels}
                    colorType={'literal'} />
            </XYPlot>
            <Legend legendData={legendData} />
        </Fragment>
    );
};

export default RadialChart;
