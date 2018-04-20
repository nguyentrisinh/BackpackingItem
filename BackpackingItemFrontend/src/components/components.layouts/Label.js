import React from 'react';
import PropTypes from 'prop-types';
export default class Label extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div className="Label">
                <div className="Label-title">
                    {this.props.title}
                </div>
            </div>

        )
    }
}

Label.propTypes = {
    title:PropTypes.string.isRequired
}