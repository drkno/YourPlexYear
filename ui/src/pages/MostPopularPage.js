import React from 'react';

import AnchorScroll from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';
import { pluralise } from './components/utils';

const MostPopularItem = ({ children, item }) => (
    <Card numx={2} numy={1}>
        <h1>All Users</h1>
        <h3>Most Popular {item}</h3>
        <br />
        <img className='img-thumbnail rounded section-top-page-img' src={children.thumbnail}></img>
        <p className='display-2 text-warning'>{children.title}</p>
        <p>{children.plays} {pluralise(children.plays, 'play')}.</p>
    </Card>
);

const MostPopularPage = ({ tv, movie, anchor, nextAnchor }) => (
    <Page anchor={anchor}>
        <CardGrid>
            <MostPopularItem item='TV Show'>
                {tv}
            </MostPopularItem>
            <MostPopularItem item='Movie'>
                {movie}
            </MostPopularItem>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default MostPopularPage;
