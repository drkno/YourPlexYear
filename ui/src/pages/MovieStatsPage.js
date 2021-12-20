import React, { Fragment } from 'react';
import humanizeDuration from 'humanize-duration';

import AnchorScroll, { AnchorScrollMobile } from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const MovieYear = ({ children }) => (
    <Fragment>
        {isNaN(children) || children < 1800 ? '' : `(${children})`}
    </Fragment>
);

const MovieStatsPage = ({ children, anchor, nextAnchor, year }) => (
    <Page className='bg-dark' anchor={anchor}>
        <CardGrid>
            <Card colour="red" numx={2} numy={2}>
                <span className='display-3'>{Math.min(children.finishedPercent, 100.00).toFixed(2)}%</span>
                <br />
                Average movie finishing percentage in {year}.
                <br />
                <br />
                You're not watching the credits like a nerd, are you?
                <AnchorScrollMobile anchor={anchor + '-duration'} />
            </Card>
            <Card colour="purple" className='text-white' numx={2} numy={2} id={anchor + '-duration'}>
                <span className='display-3'>{humanizeDuration(children.mostPaused.pausedDuration)}</span>
                <br />
                Your longest movie pause when you watched {children.mostPaused.title} <MovieYear>{children.mostPaused.year}</MovieYear>
                <AnchorScrollMobile anchor={anchor + '-oldest'} />
            </Card>
            <Card colour="greenyellow" numx={2} numy={2} id={anchor + '-oldest'}>
                <span className='display-3'>{children.oldest.title} <MovieYear>{children.oldest.year}</MovieYear></span>
                <br />
                The oldest movie you watched.
                <br />
                Enjoying the classics, huh?
                <AnchorScrollMobile anchor={anchor + '-total'} />
            </Card>
            <Card colour="orange" numx={2} numy={2} id={anchor + '-total'}>
                <span className='display-3'>{humanizeDuration(children.totalWatchTime)}</span>
                <br />
                Total time spent in {year} watching movies.
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default MovieStatsPage;
