import React, {Component} from 'react';
import {BackgroundSlider, Footer, Menu,Loading,ModalUser,Route} from '../../components/components.layouts';
import Home from '../../pages/pages.home/Home';
import DetailPageContainer from '../pages.detail/DetailPageContainer';
import ListPageContainer from '../pages.list/ListPageContainer';
import ProfileContainer from '../pages.profile/ProfileContainer';
import {withCookies} from 'react-cookie';
import {getAccountCurrent} from "../../server/serverActions";
import {getUserInfo} from "../../redux/redux.actions/appData";
import {connect} from 'react-redux';
import {withRouter} from 'react-router-dom';
// import '../../App.css';

class App extends Component {
    componentWillMount = () =>{
        const {cookies} = this.props;
        if (cookies.get("token")){
            getAccountCurrent(cookies.get("token")).then(res=>{
                console.log(res)
                if (res.status==200){
                    this.props.getUserInfo(res.data.data);
                }
            });
        }
    }


    render() {
        return (
            <div className="App">
                <Menu/>
                <div className="App-content">
                    <div className="Home">
                        {/*<img className="Home-coverImg" src={getStaticImage("Artboard.png")} alt=""/>*/}
                        <div className="Home-content">
                            <Route/>
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

export default withRouter(
    connect(null,{getUserInfo})(withCookies(App))
);
// export default App

