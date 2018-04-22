import React from 'react';
import {DOMAIN} from "../../server/serverConfig";
import {Card, Avatar, Icon, Meta, Button} from 'antd';
import {numberFormat} from "../../utils/utils";


export default class Product extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        const {data} = this.props;
        return (
            <Card
                hoverable
                bordered
                // style={{width: 300}}
                cover={
                    <img className="Product-img" src={`${DOMAIN + data.imageUrl}`} alt=""/>
                }

                actions={[

                    <Icon lg type={'shopping-cart'}>

                    </Icon>

                ]}
            >
                <Card.Meta
                    className="Product-content"
                    // avatar={<Avatar src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />}
                    title={'VND ' + numberFormat(data.basePrice.toString(),',')}
                    description={
                        <div className="Product-name">
                            {data.name}
                        </div>
                    }
                />
            </Card>
        )
    }
}


{/*<a href="#" className="Product">*/
}
{/*<div className="Product-wrapImage">*/
}
{/*<img className="Product-img" src={`${DOMAIN+data.imageUrl}`} alt=""/>*/
}
{/*</div>*/
}
{/*<div className="Product-price">*/
}
{/*VND {data.basePrice}*/
}
{/*</div>*/
}
{/*<div className="Product-name">*/
}
{/*{data.name}*/
}
{/*</div>*/
}
{/*<div className="Product-add">*/
}
{/*Thêm vào giỏ*/
}
{/*</div>*/
}
{/*</a>*/
}