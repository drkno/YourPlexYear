import React from 'react';
import humanizeDuration from 'humanize-duration';
import { pluralise } from '../utils';
import './Top10.css';

const colours = [
    'table-danger',
    'table-warning',
    'table-success',
    'table-info',
    'table-primary',
    'table-secondary',
    'table-secondary',
    'table-secondary',
    'table-secondary',
    'table-secondary',
];

const Top10Row = ({ children, includeYear, index }) => {
    return (
        <tr className={colours[index]}>
            <td>{index + 1}</td>
            <td>{children.title}</td>
            {includeYear ? (<td>{children.year}</td>) : void(0)}
            <td>{children.plays}</td>
            <td>{humanizeDuration(children.duration)}</td>
        </tr>
    );
};

const Top10 = ({ children, includeYear, mediaType, year }) => {
    return (
        <div>
            <h1>Your top 10 {pluralise(children.length, mediaType)} in {year}</h1>
            <div className='table-responsive'>
                <table className='table table-dark'>
                    <thead className='thead-dark'>
                        <tr>
                            <th>#</th>
                            <th>Title</th>
                            {includeYear ? (<th>Year</th>) : void(0)}
                            <th>Play Count</th>
                            <th>Duration</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            children.map((child, index) => (
                                <Top10Row key={'top10-' + mediaType + '-' + index} includeYear={includeYear} index={index}>
                                    {child}
                                </Top10Row>
                            ))
                        }
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default Top10;
