import React from 'react';
import { colourList, labelColour } from '../theme';

import './FunnelChart.css';

const createOtherRow = (dataList, maxLength) => {
    let other = 0;
    while (dataList.length >= maxLength) {
        other += dataList.pop().value;
    }
    dataList.push({
        name: 'Other',
        value: other
    });
}

const FunnelRow = ({ element, index, total, maxVal }) => {
    const percent = element.value * 100 / total;
    const widthPercent = element.value * 100 / maxVal;
    const backgroundColor = colourList[index % colourList.length];
    const color = percent < 5.00 ? 'white' : labelColour[index % labelColour.length];

    return (
        <tr>
            <td>{element.name}&nbsp;</td>
            <td>
                <div style={{width: `${widthPercent}%`, backgroundColor, color}}>
                    {parseFloat(percent.toFixed(2))} %
                </div>
            </td>
        </tr>
    );
};

const FunnelChart = ({ data }) => {
    const total = data.map(d => d.value)
                      .reduce((curr, next) => curr + next, 0);
    const maxVal = data.map(d => d.value)
                      .reduce((curr, next) => Math.max(curr, next), 0);
    createOtherRow(data, 15);
    return (
        <table className='funnel-chart'>
            <tbody>
                {data.map((element, index) => (
                    <FunnelRow key={element.name} element={element} index={index} total={total} maxVal={maxVal} />
                ))}
            </tbody>
        </table>
    );
};

export default FunnelChart;
