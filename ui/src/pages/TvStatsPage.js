import React from 'react';
import humanizeDuration from 'humanize-duration';

import AnchorScroll from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const BuddyCard = ({ buddy, top }) => {
    if (!buddy) {
        return (
            <Card colour="red" numx={2} numy={2}>
                <span className='display-3'>Nobody</span>
                <br />
                You're a little hipster, nobody else has been watching
                <br />
                {top.title}.
            </Card>
        );
    } else {
        return (
            <Card colour="red" numx={2} numy={2}>
                <span className='display-3'>{buddy}</span>
                <br />
                You're not alone, {buddy} likes
                <br />
                {top.title} too.
            </Card>
        );
    }
};

const TvStatsPage = ({ children, anchor, nextAnchor }) => (
    <Page className='bg-dark' anchor={anchor}>
        <CardGrid>
            <BuddyCard top={children.top10[0]} buddy={children.topBuddy} />
            <Card colour="purple" className="text-white" numx={2} numy={2}>
                <span className='display-3'>{humanizeDuration(children.mostPaused.pausedDuration)}</span>
                <br />
                Your longest pause when you watched {children.mostPaused.title}
            </Card>
            <Card colour="greenyellow" numx={2} numy={2}>
                <span className='display-3'>{children.totalEpisodes}</span>
                <br />
                Number of episodes you have watched.
                <br />
                More than you expected?
            </Card>
            <Card colour="orange" numx={2} numy={2}>
                <span className='display-3'>{humanizeDuration(children.totalWatchTime)}</span>
                <br />
                Total time spent in 2020 watching TV shows.
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default TvStatsPage;
