import { createRoot } from 'react-dom/client';
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
    const container = document.getElementById('root');
    const root = createRoot(container);
    root.render(<App year={getYear()} />);
}
