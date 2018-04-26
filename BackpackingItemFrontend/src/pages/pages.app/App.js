import React, {Component} from 'react';
import {BackgroundSlider, Footer, Menu,Loading} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import DetailPage from '../../pages/pages.detail/DetailPage';
import ListPageContainer from '../pages.list/ListPageContainer';
// import '../../App.css';
import {Route, Switch} from 'react-router-dom';

class App extends Component {
    renderRoute = () => {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/:categorySlug/:subCategorySlug'} component={ListPageContainer}/>
                <Route exact path={'/detail'} component={DetailPage}/>
                <Route exact path={'/loading'} component={Loading}/>


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
