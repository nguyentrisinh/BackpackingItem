import React from 'react';
import ProfileContainer from "../../pages/pages.profile/ProfileContainer";
import DetailPageContainer from "../../pages/pages.detail/DetailPageContainer";
import Home from "../../pages/pages.home/Home";
import OrderPage from '../../pages/pages.order/OrderPage'
import {Loading} from "./index";
import ListPageContainer from "../../pages/pages.list/ListPageContainer";
import {Route, Switch, Redirect} from 'react-router-dom';

export default class MyRoute extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <Switch>
                <Route exact path={'/'} component={Home}/>

                <Route exact path={'/product/:productId'} component={DetailPageContainer}/>
                <Route exact path={'/order'} render={() => {
                    if (this.props.userInfo) {
                        return (
                            <OrderPage/>
                        )
                    }
                    alert("Vui lòng đăng nhập để đặt hàng")
                    return (<Redirect to={'/'}></Redirect>)
                }}/>
                <Route exact path={'/profile'} render={() => {
                    if (this.props.userInfo) {
                        return (
                            <ProfileContainer/>
                        )
                    }
                    return (<Redirect to={'/'}></Redirect>)
                }}/>
                <Route exact path={'/:categorySlug/:subCategorySlug?'} component={ListPageContainer}/>

                <Route exact path={'/loading'} component={Loading}/>

            </Switch>
        )
    }
}