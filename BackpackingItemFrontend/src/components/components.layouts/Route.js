import React from 'react';
import ProfileContainer from "../../pages/pages.profile/ProfileContainer";
import DetailPageContainer from "../../pages/pages.detail/DetailPageContainer";
import Home from "../../pages/pages.home/Home";
import {Loading} from "./index";
import ListPageContainer from "../../pages/pages.list/ListPageContainer";
import {Route, Switch} from 'react-router-dom';

export default class MyRoute extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>
                <Route exact path={'/profile'} component={ProfileContainer}/>
                <Route exact path={'/product/:productId'} component={DetailPageContainer}/>
                <Route exact path={'/:categorySlug/:subCategorySlug?'} component={ListPageContainer}/>
                <Route exact path={'/loading'} component={Loading}/>
            </Switch>
        )
    }
}