import React from 'react';
import humanizeDuration from 'humanize-duration';

import AnchorScroll from './components/anchor-scroll';
import Image from './components/image';
import Page from './components/page';
import { pluralise } from './components/utils';
import VerticalCenter from './components/vertical-center';

const SectionTopPage = ({ children, mostWatched, mediaType, total, anchor, nextAnchor }) => (
    <Page className='bg-info text-white section-top-page' anchor={anchor}>
        <VerticalCenter className='h2'>
            <p>{children} <span className='text-dark'>{mediaType}s</span>,</p>
            <p>in 2020 you watched {total} {pluralise(total, mediaType)} in total.</p>
            <Image className='img-thumbnail rounded section-top-page-img' src={mostWatched.thumbnail} />
            <p className='display-2 text-warning'>{mostWatched.title}</p>
            <p>Most watched {mediaType} at<br /> {humanizeDuration(mostWatched.duration)}</p>
            <p className='h4'>({mostWatched.plays} {pluralise(mostWatched.plays, 'play')})</p>
        </VerticalCenter>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default SectionTopPage;
