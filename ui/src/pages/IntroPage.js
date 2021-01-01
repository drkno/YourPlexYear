import React from 'react';

import AnchorScroll from './components/anchor-scroll';
import Page from './components/page';
import VerticalCenter from './components/vertical-center';

const IntroPage = ({ children, nextAnchor }) => (
    <Page className='intro-page text-white'>
        <VerticalCenter>
            <h1 className='display-3'>Hey there, {children}!</h1>
            <br />
            <br />
            <h4>2020 kinda sucked, but hopefully you watched some cool stuff...</h4>
        </VerticalCenter>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default IntroPage;
