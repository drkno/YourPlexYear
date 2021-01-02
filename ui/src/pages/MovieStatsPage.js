import React from 'react';
import humanizeDuration from 'humanize-duration';

import AnchorScroll from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const MovieStatsPage = ({ children, anchor, nextAnchor }) => (
    <Page className='bg-dark' anchor={anchor}>
        <CardGrid>
            <Card colour="red" numx={2} numy={2}>
                <span className='display-3'>{Math.min(children.finishedPercent, 100.00).toFixed(2)}%</span>
                <br />
                Average movie finishing percentage in 2020.
                <br />
                <br />
                You're not watching the credits like a nerd, are you?
            </Card>
            <Card colour="purple" className='text-white' numx={2} numy={2}>
                <span className='display-3'>{humanizeDuration(children.mostPaused.pausedDuration)}</span>
                <br />
                Your longest movie pause when you watched {children.mostPaused.title} ({children.mostPaused.year})
            </Card>
            <Card colour="greenyellow" numx={2} numy={2}>
                <span className='display-3'>{children.oldest.title} ({children.oldest.year})</span>
                <br />
                The oldest movie you watched.
                <br />
                Enjoying the classics, huh?
            </Card>
            <Card colour="orange" numx={2} numy={2}>
                <span className='display-3'>{humanizeDuration(children.totalWatchTime)}</span>
                <br />
                Total time spent in 2020 watching movies.
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default MovieStatsPage;
