import React, {Component} from 'react';
import logo from '../../logo.svg';
import {BackgroundSlider, Menu,Footer} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import '../../App.css';
import {getStaticImage} from "../../utils/utils";

class App extends Component {
    render() {
        return (
            <div className="App">
                <Menu/>
                <Home/>
<Footer/>

            </div>
        );
    }
}

export default App;
