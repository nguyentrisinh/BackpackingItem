import React from 'react';
import {MAP_CATEGORY} from '../../server/serverConfig';
import {connect} from 'react-redux';
import {clickMenu} from '../../redux/redux.actions/appUI';
import classNames from 'classnames';
import {getStaticImage} from "../../utils/utils";

class MainMenu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    renderMenu = () => {
        return MAP_CATEGORY.map(item => {
            return (
                <div onClick={this.onClickMainMenu.bind(this,item.id)} id={"category"+item.id} className={classNames("MainMenu-item",{"is-active":this.props.currentCategory==item.id})}>
                    <div className="MainMenu-icon">
                        <img src={getStaticImage(item.imageName)} alt=""/>
                    </div>
                    <div className="MainMenu-name">
                        {
                            item.name
                        }
                    </div>

                </div>
            )
        })
    }

    onClickMainMenu = (id) =>{
        this.props.clickMenu(id);
    }

    render() {
        return (
            <div className="MainMenu">
                <div className="MainMenu-wrap container">
                    {
                        this.renderMenu()
                    }
                </div>

            </div>
        )
    }
}

const mapStateToProps = state =>    {
    return {
        currentCategory:state.appUI.currentCategory
    }
}

export default connect(mapStateToProps,{clickMenu})(MainMenu)