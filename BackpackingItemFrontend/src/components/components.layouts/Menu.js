import React from 'react';
import {MenuHeader,MainMenu,SubMenu} from './index'

export default class Menu extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div className="Menu">
                <MenuHeader/>
                <MainMenu/>
                <SubMenu/>
            </div>
        )
    }
}