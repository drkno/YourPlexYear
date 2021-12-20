import React from 'react';

import AnchorScroll, { AnchorScrollMobile } from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Image from './components/image';
import Page from './components/page';
import { pluralise } from './components/utils';

const MostPopularItem = ({ children, item, id=void(0), nextAnchor=void(0) }) => (
    <Card numx={2} numy={1} id={id}>
        <h1>All Users</h1>
        <h3>Most Popular {item}</h3>
        <br />
        <Image className='img-thumbnail rounded section-top-page-img' src={children.thumbnail} />
        <p className='display-2 text-warning'>{children.title}</p>
        <p>{children.plays} {pluralise(children.plays, 'play')}</p>
        {nextAnchor == void(0) ? void(0) : (<AnchorScrollMobile anchor={nextAnchor} />)}
    </Card>
);

const MostPopularPage = ({ tv, movie, anchor, nextAnchor }) => (
    <Page anchor={anchor}>
        <CardGrid>
            <MostPopularItem item='TV Show' nextAnchor={anchor + '-movie'}>
                {tv}
            </MostPopularItem>
            <MostPopularItem item='Movie' id={anchor + '-movie'}>
                {movie}
            </MostPopularItem>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default MostPopularPage;
