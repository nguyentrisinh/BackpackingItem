import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter as Router} from 'react-router-dom';
import {CookiesProvider} from 'react-cookie';
import {Provider} from "react-redux";
import './index.css';
import App from './pages/pages.app/App';
import store from './redux/store';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(
    <CookiesProvider>
        <Provider store={store}>
            <Router>
                <App/>
            </Router>
        </Provider>
    </CookiesProvider>, document.getElementById('root'));
registerServiceWorker();

