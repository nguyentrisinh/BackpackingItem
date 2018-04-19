import React from 'react';
import {MAP_CATEGORY} from "../../server/serverConfig";
import ToolTip from 'react-portal-tooltip';
import {connect} from 'react-redux';
import onClickOutside from "react-onclickoutside";
import {clickMenu} from "../../redux/redux.actions/appUI";

const style = {
    style: {
        borderRadius:0,
        padding:0,
        boxShadow:'none',
        backgroundColor:'rgba(255,255,255,0.2)',
        // textTransform:"uppercase",
        marginTop:-15,
        borderTop:'solid 1px white'
    },
    arrowStyle: {

    }
}

class SubMenu extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }

    handleClickOutside = evt => {
        this.props.closeSubMenu();
    };

    renderMenu = () =>{
        const {indexCategory} = this.props;
        if (indexCategory!=-1){
            return MAP_CATEGORY[indexCategory].children.map(item=>{
                return (
                    <div className="SubMenu-item">
                        {
                            item.name
                        }
                    </div>
                )
            })
        }
    }
    render(){
        return (
                <div className="SubMenu">
                    {
                        this.renderMenu()
                    }
                </div>
        )
    }
}

const SubMenuWithClickOutside = onClickOutside(SubMenu);

class ToolTipSubMenu extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    closeSubMenu = (    )=>{
        this.props.clickMenu(null);
    }
    getCurrentCategory = () =>{
        return MAP_CATEGORY.findIndex(o=>o.id == this.props.currentCategory);
    }
    render(){
        const foundIndex = this.getCurrentCategory();
        return (
            <ToolTip style={style} active={true} position="bottom" parent={`#${"category"+this.props.currentCategory}`}>
                <SubMenuWithClickOutside outsideClickIgnoreClass={"MainMenu"} closeSubMenu={this.closeSubMenu} indexCategory={foundIndex}/>
            </ToolTip>
        )
    }
}

const mapStateToProps = state =>    {
    return {
        currentCategory:state.appUI.currentCategory
    }
}
export default connect(mapStateToProps,{clickMenu})(ToolTipSubMenu);