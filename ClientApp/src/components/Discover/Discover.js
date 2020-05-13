import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection';
import { Row, Col } from 'reactstrap';
import './Discover.css'
import api from '../../sideEffects/apis/api';

export default function Discover(props) {
    const [pageLoaded, setPageLoaded] = useState(true);
    const [contents, setContents] = useState([]);
    useEffect(() => {
        if (pageLoaded) {
            api.getAll("collectiontypes", "collectiontypes")
                .then(response => {
                    setContents(response)
                    setPageLoaded(false)
                })
                .catch(err => {
                    console.log(err)
                    setPageLoaded(false)
                })
        }

    }, [pageLoaded])

    return (
        contents.map((collectionType) => (
            <div key={collectionType.name+collectionType.id} className="enforce-mp">
                <Row className="mt-4">
                    <Col>
                        <h2>{collectionType.name}</h2>
                    </Col>
                </Row>
                {
                    collectionType.collections.length > 3 && 
                    <Row className="mt-3 mb-3">
                        <Col>
                            <div className="app-flex">
                                {collectionType.collections.slice(3).map((collection, index) =>
                                    (
                                        <div key={collection.name+collection.id} className={index === 0 ? "mr-1" : "mr-1 ml-1"}>
                                            <button className="discover-cat">{collection.name}</button>
                                        </div>
                                    ))}
                            </div>
                        </Col>
                    </Row>
                }
                
                <Row>
                    {
                        collectionType.collections.slice(0, 3).map((collection) => (
                            <Col key={collection.name+collection.id} sm={12} md={4} lg={4}>
                                <Collection collectibles={collection.collectibles} />
                            </Col>
                           ))
                    }
                </Row>
            </div>
        ))
        
    )
}
