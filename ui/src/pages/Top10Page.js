import React from 'react';

import AnchorScroll from './components/anchor-scroll';
import Page from './components/page';
import Top10 from './components/top10';
import VerticalCenter from './components/vertical-center';

const Top10Page = ({ items, includeYear, mediaType, anchor, nextAnchor, year }) => (
    <Page anchor={anchor} className='bg-dark text-white'>
        <div className='row justify-content-center'>
            <div className='col-12 col-lg-6'>
                <br />
                <VerticalCenter strategy='dynamic'>
                    <Top10 includeYear={includeYear} mediaType={mediaType} year={year}>
                        {items}
                    </Top10>
                </VerticalCenter>
                <br />
            </div>
        </div>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default Top10Page;
