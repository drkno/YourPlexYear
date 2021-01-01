import React from 'react';
import PieChart from './components/pie-chart';

import AnchorScroll from './components/anchor-scroll';
import { Card, CardGrid } from './components/card';
import Page from './components/page';

const BrowserUsagePage = ({ anchor, nextAnchor, yourBrowsers, globalBrowsers }) => (
    <Page className='bg-dark text-white' anchor={anchor}>
        <CardGrid>
            <Card numx={2} numy={1}>
                <h1>Your Plex Clients</h1>
                <h6>
                    <PieChart dataKey='value' data={yourBrowsers} />
                </h6>
            </Card>
            <Card numx={2} numy={1}>
                <h1>Global Plex Clients</h1>
                <h6>
                    <PieChart dataKey='value' data={globalBrowsers} />
                </h6>
            </Card>
        </CardGrid>
        <AnchorScroll anchor={nextAnchor} />
    </Page>
);

export default BrowserUsagePage;
