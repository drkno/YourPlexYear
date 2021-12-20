import React, { Fragment } from 'react';
import Legend from '../legend';
import { colourList, labelColour } from '../theme';

import './treemap-chart.css';

const Label = ({ element, percentage }) => {
    if (percentage < 0.2) {
        return null;
    }
    return (
        <span style={{color: element.labelColour || 'black'}}>
            <h5>{element.title}</h5>
            ({parseFloat((percentage*100).toPrecision(2))} %)
        </span>
    );
}

const Horizontal = ({ list, index, total, remainingHeight, remainingWidth, realTotal }) => {
    if (index == list.length) {
        return null;
    }
    const percentage = list[index].size / total;
    const height = percentage * remainingHeight;
    return (
        <Fragment>
            <div className='treemap-segment' style={{
                top: `${100 - remainingHeight}%`,
                left: `${100 - remainingWidth}%`,
                height: `${height}%`,
                width: `${remainingWidth}%`,
                backgroundColor: list[index].color
            }}>
                <Label element={list[index]} percentage={list[index].size / realTotal} />
            </div>
            <Vertical list={list}
                      index={index + 1}
                      total={total - list[index].size}
                      realTotal={realTotal}
                      remainingHeight={remainingHeight - height}
                      remainingWidth={remainingWidth} />
        </Fragment>
    );
};

const Vertical = ({ list, index, total, remainingHeight, remainingWidth, realTotal }) => {
    if (index == list.length) {
        return null;
    }
    const percentage = list[index].size / total;
    const width = percentage * remainingWidth;
    return (
        <Fragment>
            <div className='treemap-segment' style={{
                top: `${100 - remainingHeight}%`,
                left: `${100 - remainingWidth}%`,
                height: `${remainingHeight}%`,
                width: `${width}%`,
                backgroundColor: list[index].color
            }}>
                <Label element={list[index]} percentage={list[index].size / realTotal} />
            </div>
            <Horizontal list={list}
                        index={index + 1}
                        total={total - list[index].size}
                        realTotal={realTotal}
                        remainingHeight={remainingHeight}
                        remainingWidth={remainingWidth - width} />
        </Fragment>
    );
};

const Tree = ({ data, width, height }) => {
    const total = data.map(d => d.size)
        .reduce((curr, next) => curr + next, 0);
    return (
        <div className='treemap-root' style={{width: `${width}px`, height: `${height}px`}}>
            <Vertical list={data} index={0} total={total} realTotal={total} remainingHeight={100} remainingWidth={100} />
        </div>
    );
};

const TreemapChart = ({ data }) => {
    const mapItems = data.map((d, i) => ({
        title: d.name,
        size: d.value,
        color: colourList[i % colourList.length],
        labelColour: labelColour[i % labelColour.length]
    }));
    const total = data.map(d => d.value)
        .reduce((curr, next) => curr + next, 0);
    const legendItems = data.map((d, i) => ({
        title: `${d.name} (${parseFloat((d.value * 100 / total).toPrecision(2))} %)`,
        color: colourList[i % colourList.length]
    }));
    return (
        <div className='treemap-chart'>
            <Tree data={mapItems} width={400} height={400} />
            <br />
            <Legend legendData={legendItems} />
        </div>
    );
};

export default TreemapChart;
