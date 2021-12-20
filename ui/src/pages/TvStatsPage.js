import React from 'react';
import humanizeDuration from 'humanize-duration';

import AnchorScroll, { AnchorScrollMobile } from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const BuddyCard = ({ buddy, top, anchor }) => {
    if (!buddy) {
        return (
            <Card colour="red" numx={2} numy={2}>
                <span className='display-3'>Nobody</span>
                <br />
                Well that's... interesting, nobody else seems to have been watching
                <br />
                {top.title}.
                <AnchorScrollMobile anchor={anchor + '-pause'} />
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
                <AnchorScrollMobile anchor={anchor + '-pause'} />
            </Card>
        );
    }
};

const TvStatsPage = ({ children, anchor, nextAnchor, year }) => (
    <Page className='bg-dark' anchor={anchor}>
        <CardGrid>
            <BuddyCard top={children.top10[0]} buddy={children.topBuddy} anchor={anchor} />
            <Card colour="purple" className="text-white" numx={2} numy={2} id={anchor + '-pause'}>
                <span className='display-3'>{humanizeDuration(children.mostPaused.pausedDuration)}</span>
                <br />
                Your longest pause when you watched {children.mostPaused.title}
                <AnchorScrollMobile anchor={anchor + '-count'} />
            </Card>
            <Card colour="greenyellow" numx={2} numy={2} id={anchor + '-count'}>
                <span className='display-3'>{children.totalEpisodes}</span>
                <br />
                Number of episodes you have watched.
                <br />
                More than you expected?
                <AnchorScrollMobile anchor={anchor + '-total'} />
            </Card>
            <Card colour="orange" numx={2} numy={2} id={anchor + '-total'}>
                <span className='display-3'>{humanizeDuration(children.totalWatchTime)}</span>
                <br />
                Total time spent in {year} watching TV shows.
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default TvStatsPage;
