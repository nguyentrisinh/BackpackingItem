import React from 'react';
import {numberFormat} from "../../utils/utils";
import {Breadcrumb, Row, Card, Col, Select, InputNumber, Button, Icon, Radio} from 'antd';
import Swiper from 'react-id-swiper';
import {Path} from '../../components/components.layouts/index';
import {DOMAIN} from "../../server/serverConfig";

export default class DetailPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentImage:this.props.productData.imageUrl
        };
    }

    renderSizes = () => {
        const {variants} = this.props.productData;
        let variantsId = Array.from(new Set(variants.map(item => {
            return item.size.id
        })));
        let listRadioButton =  variantsId.map(id => {
            return (
                <Radio.Button value={id}>{variants.find(o => o.size.id == id).size.name}</Radio.Button>
            )
        });
        // alert(variantsId.length)
        if (variantsId.length>0){
            return (
                <div className="DetailPage-actionItem mb-2">
                    <div className="DetailPage-actionLabel">
                        Size:
                    </div>
                    <div className="DetailPage-actionSelect">
                        <Radio.Group onChange={this.onChangeSizes}>
                            {
                                listRadioButton
                            }
                        </Radio.Group>
                    </div>
                </div>
            )
        }
    }

    renderColors = () => {
        const {variants} = this.props.productData;
        let variantsId = Array.from(new Set(variants.map(item => {
            return item.color.id
        })));
        let listRadioButton =  variantsId.map(id => {
            return (
                <Radio.Button
                    // style={{color:variants.find(o=>o.color.id==id).color.colorCode}}
                    value={id}>{variants.find(o => o.color.id == id).color.name}</Radio.Button>
            )
        })
        if (variantsId.length>0){
            return(
                <div className="DetailPage-actionItem mb-2">
                    <div className="DetailPage-actionLabel">
                        Màu:
                    </div>
                    <div className="DetailPage-actionSelect">

                        <Radio.Group>
                            {
                                listRadioButton
                            }
                        </Radio.Group>
                    </div>
                </div>
            )
        }

    }
    onClickImage = (url)=>{
        this.setState({
            currentImage:url
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
        let setIdImages = Array.from(new Set(listImages.map(o=>{return o.imageUrl})));
        return setIdImages.map(url =>{
            return  <div onClick={this.onClickImage.bind(this,url)}>
                <img src={DOMAIN+url} alt=""/>
            </div>
        })

    }


    onChangeSizes = e => {

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

                                    <InputNumber min={1} max={10} defaultValue={1}/>
                                </div>
                            </div>
                        </div>
                        <div className="mt-4 mb-3 font-weight-bold DetailPage-price">

                            Gía: {numberFormat(productData.basePrice.toString(), ',')} VND
                        </div>
                        <div className="DetailPage-add">
                            <Button className="DetailPage-addButton" type="omitted" size='large'>
                                <Icon type="shopping-cart"/>
                                Thêm vào giỏ
                            </Button>
                        </div>
                    </Col>
                    <Col span={10}>
                        <Card cover={<img src={DOMAIN+this.state.currentImage} alt=""/>} bordered={true} hoverable>

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