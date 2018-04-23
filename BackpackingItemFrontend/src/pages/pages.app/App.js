import React, {Component} from 'react';
import {BackgroundSlider, Footer, Menu} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import DetailPage from '../../pages/pages.detail/DetailPage';
import ListPage from '../../pages/pages.list/ListPage';
// import '../../App.css';
import {Route, Switch} from 'react-router-dom';

class App extends Component {
    renderRoute = () => {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/list'} component={ListPage}/>
                <Route exact path={'/detail'} component={DetailPage}/>

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
