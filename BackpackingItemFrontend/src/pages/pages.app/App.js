import React, {Component} from 'react';
import {BackgroundSlider, Footer, Menu,Loading,ModalUser} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import DetailPageContainer from '../pages.detail/DetailPageContainer';
import ListPageContainer from '../pages.list/ListPageContainer';
import ProfileContainer from '../pages.profile/ProfileContainer';
import {withCookies} from 'react-cookie';
import {getAccountCurrent} from "../../server/serverActions";
import {getUserInfo} from "../../redux/redux.actions/appData";
import {connect} from 'react-redux';
// import '../../App.css';
import {Route, Switch} from 'react-router-dom';

class App extends Component {
    // componentWillMount = () =>{
    //     const {cookies} = this.props;
    //     if (cookies.get("token")){
    //         getAccountCurrent(cookies.get("token")).then(res=>{
    //             console.log(res)
    //             if (res.status==200){
    //                 this.props.getUserInfo(res.data.data);
    //             }
    //         });
    //     }
    // }
    renderRoute = () => {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/:categorySlug/:subCategorySlug?'} component={ListPageContainer}/>\
                {/*<Route exact path={'/product/:productId'} component={DetailPageContainer}/>*/}
                {/*<Route exact path={'/profile'} component={ProfileContainer}/>*/}
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

export default connect(null,{getUserInfo})(App);
