import React from 'react';

import Page from './components/page';
import VerticalCenter from './components/vertical-center';

const goToLastYear = () => {
    const parts = document.location.pathname.split('/');
    let year = parseInt(parts[1]);
    year--;
    window.location.pathname = '/' + year;
};

const LastYearButton = () => (
    <button type='button' className='btn btn-outline-warning btn-lg' onClick={goToLastYear}>See Last Year</button>
);

const RequestButton = ({ url }) => (
    <button type='button' className='btn btn-outline-warning btn-lg' onClick={() => document.location.href = url}>Request Something</button>
);

const TautulliButton = ({ url }) => (
    <button type='button' className='btn btn-outline-warning btn-lg' onClick={() => document.location.href = url}>More Data!</button>
);

const ConclusionPage = ({ ombi, tautulli, anchor }) => (
    <Page className='conclusion-page text-white' anchor={anchor}>
        <VerticalCenter>
            <h1 className='display-3'>Thats all for now</h1>
            <br />
            <h4>Hope you have a great New Year!</h4>
            <br />
            <LastYearButton />
            &nbsp;&nbsp;
            <RequestButton url={ombi} />
            &nbsp;&nbsp;
            <TautulliButton url={tautulli} />
        </VerticalCenter>
    </Page>
);

export default ConclusionPage;
