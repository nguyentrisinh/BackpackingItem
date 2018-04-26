import React from 'react';
import {MAP_CATEGORY} from '../../server/serverConfig';
import {connect} from 'react-redux';
import {clickMenu} from '../../redux/redux.actions/appUI';
import classNames from 'classnames';
import {getStaticImage} from "../../utils/utils";
import {Link} from 'react-router-dom';

class MainMenu extends React.Component {
    renderMenu = () => {
        return MAP_CATEGORY.map(item => {
            return (
                <Link to={`/${item.link}`} onClick={this.onClickMainMenu.bind(this, item)} id={"category" + item.id}
                     className={classNames("MainMenu-item", {"is-active": (this.props.currentCategory?(this.props.currentCategory.id == item.id?true:false):false)})}>
                    <div className="MainMenu-icon">
                        <img src={getStaticImage(item.imageName)} alt=""/>
                    </div>
                    <div className="MainMenu-name">
                        {
                            item.name
                        }
                    </div>

                </Link>
            )
        })
    }
    onClickMainMenu = (item) => {
        this.props.clickMenu(item);
    }

    constructor(props) {
        super(props);
        this.state = {};
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

const mapStateToProps = state => {
    return {
        currentCategory: state.appUI.currentCategory
    }
}

export default connect(mapStateToProps, {clickMenu})(MainMenu)