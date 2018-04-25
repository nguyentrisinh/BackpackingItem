import React from 'react';
import {Card,Radio,Acnhor,Slider,Anchor} from'antd';

const radioStyle = {
    display: 'block',
    height: '30px',
    lineHeight: '30px',
};

export default class SideBar extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <Anchor offsetTop={172}>
                <Card title="Sắp xếp" className={'mb-2'}>
                    <Radio.Group>
                        <Radio style={radioStyle} value={1}>Nổi bật</Radio>
                        <Radio style={radioStyle} value={2}>Mới</Radio>
                        <Radio style={radioStyle} value={3}>Bán chạy</Radio>
                        <Radio style={radioStyle} value={4}>Khuyến mãi</Radio>
                        <Radio style={radioStyle} value={5}>Giá từ thấp đến cao</Radio>
                        <Radio style={radioStyle} value={6}>Giá từ cao đến thấp</Radio>
                        <Radio style={radioStyle} value={7}>Tên từ A-Z</Radio>
                        <Radio style={radioStyle} value={8}>Tên từ Z-A</Radio>
                    </Radio.Group>

                </Card>
                <Card title="Lọc theo giá">
                    <Slider range defaultValue={[20, 50]}/>

                </Card>
            </Anchor>
        )
    }
}