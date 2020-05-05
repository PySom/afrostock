import React from 'react';
import Collection from '../Collection/Collection';
import getImages from '../../helper/imageHelper';
import { Row, Col } from 'reactstrap';
import './Discover.css'

export default function Discover(props) {
    const images = getImages(5);
    const collectionName = "Popular Categories";
    const categories = ["Okay", "Good", "Lookout"]

    return (
        <div className="enforce-mp">
            <Row className="mt-4">
                <Col>
                    <h2>{collectionName}</h2>
                </Col>
            </Row>
            <Row className="mt-3 mb-3">
                <Col>
                    <div className="app-flex">
                        {categories.map((name, index) =>
                            (
                                <div key={index} className={index === 0 ? "mr-1" : "mr-1 ml-1"}>
                                    <button className="discover-cat">{name}</button>
                                </div>
                            ))}
                    </div>
                </Col>
            </Row>
            <Row>
                <Col sm={12} md={4} lg={4}>
                    <Collection images={images} categoryName="Helping hands" />
                </Col>
                <Col sm={12} md={4} lg={4}>
                    <Collection images={images} categoryName="All to Jesus" />
                </Col>
                <Col sm={12} md={4} lg={4}>
                    <Collection images={images} categoryName="All to Jesus" />
                </Col>
            </Row>
        </div>
    )
}
