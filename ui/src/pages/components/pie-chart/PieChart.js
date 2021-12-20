import React from 'react';
import { PieChart as RechartsPieChart, Pie, Tooltip, Cell } from 'recharts';
import { colourList, labelColour } from '../theme';

import './PieChart.css';

const RADIAN = Math.PI / 180;
const renderLabel = ({ cx, cy, midAngle, innerRadius, outerRadius, percent, name, index }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.7;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);
   
    return (
      <text x={x} y={y} fill={labelColour[index % labelColour.length]} textAnchor='middle' dominantBaseline="central">
          {`${name} (${(percent * 100).toFixed(0)}%)`}
      </text>
    );
};

const PieChart = ({ data, dataKey='value' }) => (
    <RechartsPieChart width={400} height={400} className='pie-chart'>
        <Pie dataKey={dataKey}
             data={data}
             isAnimationActive
             label={renderLabel}
             labelLine={false}>
            {
                data.map((_, index) => (<Cell fill={colourList[index % colourList.length]} />))
            }
        </Pie>
        <Tooltip />
    </RechartsPieChart>
);

export default PieChart;
