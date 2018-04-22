import React, {Component} from 'react';
import logo from '../../logo.svg';
import {BackgroundSlider, Menu, Footer} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import ListPage from '../../pages/pages.list/ListPage';
// import '../../App.css';
import {Router, Route, Switch} from 'react-router-dom';
import {getStaticImage} from "../../utils/utils";

class App extends Component {
    renderRoute = () => {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/list'} component={ListPage}/>
            </Switch>
        )
    }

    render() {
        return (
            <div className="App">
                <Menu/>
                <div className="App-content">
                        <div className="Home">
                            {/*<img className="Home-coverImg" src={getStaticImage("Artboard.png")} alt=""/>*/}
                            <div className="Home-content">
                    {
                        this.renderRoute()
                    }
                            </div>

                        </div>
                </div>
                <Footer/>
                <BackgroundSlider/>
            </div>
        );
    }
}

export default App;
