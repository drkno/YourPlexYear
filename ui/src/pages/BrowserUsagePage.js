import React from 'react';
import FunnelChart from './components/funnel-chart';
import TreemapChart from './components/treemap-chart';
import AnchorScroll, { AnchorScrollMobile } from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const BrowserUsagePage = ({ anchor, nextAnchor, yourBrowsers, globalBrowsers }) => (
    <Page className='bg-dark text-white' anchor={anchor}>
        <CardGrid>
            <Card numx={2} numy={1}>
                <h1>Your Plex Clients</h1>
                <h6>
                <TreemapChart dataKey='value' data={yourBrowsers} />
                </h6>
                <AnchorScrollMobile anchor={anchor + '-global'} />
            </Card>
            <Card numx={2} numy={1} id={anchor + '-global'}>
                <h1>Global Plex Clients</h1>
                <h6>
                    <FunnelChart data={globalBrowsers} />
                </h6>
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default BrowserUsagePage;
