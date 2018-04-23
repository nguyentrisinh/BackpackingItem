import React from 'react';
import {Product} from '../../components/components.layouts/index'
import {Col, Row} from 'antd';

export default class ListProduct extends React.Component {
    renderProduct = () => {
        return this.props.data.data.map(item => {
            return (
                <Col span={6}>
                    <Product data={item}/>
                </Col>
            )
        })
    }

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        if (this.props.data.isLoading) {
            return (
                <div className="ListProduct">
                    Đang tải
                </div>
            )
        }
        if (Array.isArray(this.props.data.data)) {
            if (this.props.data.data.length > 0) {
                return (
                    <Row gutter={10}>
                        {this.renderProduct()}
                    </Row>
                )
            }
        }
        return (
            <div className="ListProduct">
                Lỗi
            </div>
        )

    }
}

{/*<div className="ListProduct">*/
}
{/*{*/
}
{/*this.renderProduct()*/
}
{/*}*/
}
{/*</div>*/
}


{/*<div className="ListProduct-item">*/
}
{/*<Product data={item}/>*/
}
{/*</div>*/
}