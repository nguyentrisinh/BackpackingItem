import React, {Component} from 'react';
import {BackgroundSlider, Footer, Menu,Loading,ModalUser} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import DetailPageContainer from '../pages.detail/DetailPageContainer';
import ListPageContainer from '../pages.list/ListPageContainer';
// import '../../App.css';
import {Route, Switch} from 'react-router-dom';

class App extends Component {
    renderRoute = () => {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/product/:productId'} component={DetailPageContainer}/>
                <Route exact path={'/:categorySlug/:subCategorySlug?'} component={ListPageContainer}/>
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
                <ModalUser/>

                <Footer/>
                <BackgroundSlider/>
            </div>
        );
    }
}

export default App;
