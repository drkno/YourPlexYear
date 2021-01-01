import React from 'react';

import AnchorScroll from './components/anchor-scroll';
import Page from './components/page';
import VerticalCenter from './components/vertical-center';

const ConclusionPage = ({ ombi, tautulli, anchor }) => (
    <Page className='conclusion-page text-white' anchor={anchor}>
        <VerticalCenter>
            <h1 className='display-3'>Happy New Year!</h1>
            <br />
            <br />
            <h4>Request more content at <a className='text-warning' href={ombi}>Ombi</a>.</h4>
            <h4>If you would like to see more of your stats, see <a className='text-warning' href={tautulli}>Tautulli</a></h4>
        </VerticalCenter>
    </Page>
);

export default ConclusionPage;
