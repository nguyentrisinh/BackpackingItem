import React from 'react';
import App from './App';
import {connect} from 'react-redux';
import {getUserInfo} from "../../redux/redux.actions/appData";
 class AppContainer extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
                <App/>
        )
    }
}


export default connect(null,{getUserInfo})(AppContainer);