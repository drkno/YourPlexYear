import React from 'react';
import ReactDOM from 'react-dom';

import App from './pages/RootContainer';

import 'bootstrap/dist/css/bootstrap.css';
import './index.css';

const redirectToYear = () => {
    if (document.location.pathname !== '/') {
        return false;
    }
    const now = new Date();
    let year = now.getFullYear();
    if (now.getMonth() < 6) {
        year--;
    }
    document.location.pathname = '/' + year;
    return true;
};

const getYear = () => {
    const parts = document.location.pathname.split('/');
    return parseInt(parts[1]);
};

if (!redirectToYear()) {
    ReactDOM.render(<App year={getYear()} />, document.getElementById('root'));
}