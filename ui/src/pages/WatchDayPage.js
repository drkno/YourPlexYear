import React from 'react';
import RadialChart from './components/radial-chart';

import AnchorScroll from './components/anchor-scroll';
import Page from './components/page';
import VerticalCenter from './components/vertical-center';

const WatchDayPage = ({ days, anchor, nextAnchor, year }) => {
    return (
        <Page anchor={anchor} className='bg-dark text-white'>
            <div className='row justify-content-center'>
                <div className='col-12 col-lg-6'>
                    <br />
                    <h1>Days you used Plex in {year}</h1>
                    <VerticalCenter strategy='dynamic'>
                        <RadialChart data={days} dataKey='count' titleKey='day' />
                    </VerticalCenter>
                    <br />
                </div>
            </div>
            <AnchorScroll anchor={nextAnchor} />
        </Page>
    );
};

export default WatchDayPage;
