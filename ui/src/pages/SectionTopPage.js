import React from 'react';

import AnchorScroll from './components/anchor-scroll';
import Page from './components/page';
import { pluralise } from './components/utils';
import VerticalCenter from './components/vertical-center';

const SectionTopPage = ({ children, mostWatched, mediaType, total, anchor, nextAnchor }) => (
    <Page className='bg-info text-white section-top-page' anchor={anchor}>
        <VerticalCenter className='h1'>
            <p>{children} <span className='text-dark'>{mediaType}s</span>,</p>
            <p>in 2020 you watched {total} {pluralise(total, mediaType)} in total.</p>
            <br />
            <img className='img-thumbnail rounded section-top-page-img' src={mostWatched.thumbnail}></img>
            <p className='display-2 text-warning'>{mostWatched.title}</p>
            <p>Most watched {mediaType} at<br />{mostWatched.plays} {pluralise(mostWatched.plays, 'play')}.</p>
        </VerticalCenter>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default SectionTopPage;
