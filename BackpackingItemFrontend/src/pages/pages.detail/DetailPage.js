import React from 'react';
import {numberFormat} from "../../utils/utils";
import {connect} from 'react-redux';
import {addToCart} from "../../redux/redux.actions/appData";
import {Breadcrumb, Row, Card, Col, Select, InputNumber, Button, Icon, Radio} from 'antd';
import Swiper from 'react-id-swiper';
import {withCookies} from 'react-cookie';
import {Path} from '../../components/components.layouts/index';
import {DOMAIN} from "../../server/serverConfig";

class DetailPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentColor: null,
            currentSize: null,
            notification: null,
            currentQuantity: 1,
            isOpenButtonBuy: this.props.productData.variants.length == 0,
            currentImage: this.props.productData.imageUrl
        };
    }

    renderSizes = () => {
        const {variants} = this.props.productData;
        let variantsId = Array.from(new Set(variants.map(item => {
            return item.size.id
        })));
        let listRadioButton = variantsId.map(id => {
            return (
                <Radio.Button value={id}>{variants.find(o => o.size.id == id).size.name}</Radio.Button>
            )
        });
        // alert(variantsId.length)
        if (variantsId.length > 0) {
            return (
                <div className="DetailPage-actionItem mb-2">
                    <div className="DetailPage-actionLabel">
                        Size:
                    </div>
                    <div className="DetailPage-actionSelect">
                        <Radio.Group value={this.state.currentSize} onChange={this.onChangeSizes}>
                            {
                                listRadioButton
                            }
                        </Radio.Group>
                    </div>
                </div>
            )
        }
    }

    onChangeColor = (e) => {
        this.setState({
            currentColor: e.target.value
        });
        this.checkExistVariant(e.target.value, this.state.currentSize);

    }

    checkExistVariant = (color, size) => {
        if (color == null || size == null) {
            // this.setState({
            //     notification:"Vui lòng chọn loại sản phẩm"
            // })
            return;
        }
        const {variants} = this.props.productData;
        const variant = variants.find(o => o.color.id == color && o.size.id == size);
        if (variant) {
            if (variant.variantStatus == 1) {
                this.setState({
                    isOpenButtonBuy: true,
                    notification: ""
                })
            }
            return;

        }
        this.setState({
            isOpenButtonBuy: false,
            notification: "Sản phẩm hết hàng"
        })


    }

    renderColors = () => {
        const {variants} = this.props.productData;
        let variantsId = Array.from(new Set(variants.map(item => {
            return item.color.id
        })));
        let listRadioButton = variantsId.map(id => {
            return (
                <Radio.Button
                    // style={{color:variants.find(o=>o.color.id==id).color.colorCode}}
                    value={id}>{variants.find(o => o.color.id == id).color.name}</Radio.Button>
            )
        })
        if (variantsId.length > 0) {
            return (
                <div className="DetailPage-actionItem mb-2">
                    <div className="DetailPage-actionLabel">
                        Màu:
                    </div>
                    <div className="DetailPage-actionSelect">

                        <Radio.Group value={this.state.currentColor} onChange={this.onChangeColor}>
                            {
                                listRadioButton
                            }
                        </Radio.Group>
                    </div>
                </div>
            )
        }

    }
    onClickImage = (url) => {
        this.setState({
            currentImage: url
        })
    }

    renderImages = () => {
        const {variants} = this.props.productData;
        let listImages = [];
        variants.map(item => {
            item.images.map(child => {
                listImages.push(child);
            })
        });
        let setIdImages = Array.from(new Set(listImages.map(o => {
            return o.imageUrl
        })));
        return setIdImages.map(url => {
            return <div onClick={this.onClickImage.bind(this, url)}>
                <img src={DOMAIN + url} alt=""/>
            </div>
        })

    }


    onChangeSizes = e => {
        this.setState({
            currentSize: e.target.value
        });
        this.checkExistVariant(this.state.currentColor, e.target.value);
    }

    onClickAddToCart = () => {
        const {cookies} = this.props;
        const variant = this.props.productData.variants.find(o => o.size.id == this.state.currentSize && o.color.id == this.state.currentColor);
        var date= new Date();
        date.setMonth(date.getFullYear()+10);
        if (variant) {
            if (cookies.get(`product_${variant.id}`)){
                cookies.set(`product_${variant.id}`,this.state.currentQuantity + parseInt(cookies.get(`product_${variant.id}`)),{
                    path:'/',
                    expires:date
                });

                this.props.addToCart(variant.id,variant.color,variant.size,this.state.currentQuantity);
            }
            else{
                cookies.set(`product_${variant.id}`,this.state.currentQuantity,{
                    path:'/',
                    expires:date
                });
                console.log(this.props);
                this.props.addToCart(variant.id,variant.color,variant.size,this.state.currentQuantity);
            }

        }

    }

    onChangeQuantity = value => {
        this.setState({
            currentQuantity: value
        })
    }

    render() {
        const {productData} = this.props;
        return (
            <div className="DetailPage container">
                <Card bordered={false}>
                    <Path/>
                </Card>
                <Row>
                    <Col span={14} className='pr-3'>
                        <div className="font-weight-bold DetailPage-productName mb-3">
                            {productData.name}
                        </div>
                        <div dangerouslySetInnerHTML={{__html: productData.description}}
                             className="DetailPage-productDescription mb-4">
                        </div>
                        <div className="DetailPage-actions">
                            {
                                this.renderSizes()
                            }
                            {
                                this.renderColors()
                            }
                            <div className="DetailPage-actionItem mb-2">
                                <div className="DetailPage-actionLabel">
                                    Số lượng:
                                </div>
                                <div className="DetailPage-actionSelect">

                                    <InputNumber onChange={this.onChangeQuantity} value={this.state.currentQuantity}
                                                 min={1} max={10}/>
                                </div>
                            </div>
                        </div>
                        <div className="mt-4 mb-3 font-weight-bold DetailPage-price">

                            Giá: {numberFormat(productData.basePrice.toString(), ',')} VND
                        </div>
                        <div className="DetailPage-add">
                            <div className="text-info pb-2">{this.state.notification}</div>
                            <Button onClick={this.onClickAddToCart} disabled={!this.state.isOpenButtonBuy}
                                    className="DetailPage-addButton"
                                    type="omitted" size='large'>
                                <Icon type="shopping-cart"/>
                                Thêm vào giỏ
                            </Button>
                        </div>
                    </Col>
                    <Col span={10}>
                        <Card cover={<img src={DOMAIN + this.state.currentImage} alt=""/>} bordered={true} hoverable>

                            <Swiper pagination={{
                                el: '.swiper-pagination',
                                type: 'bullets',
                                clickable: true
                            }}
                                    navigation={{
                                        nextEl: '.swiper-button-next',
                                        prevEl: '.swiper-button-prev'
                                    }}
                                    spaceBetween={10} slidesPerView={3}>
                                {
                                    this.renderImages()
                                }

                            </Swiper>
                        </Card>

                    </Col>
                </Row>
            </div>
        )
    }
}

export default connect(null,{addToCart})(withCookies(DetailPage))