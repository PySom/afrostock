import React from 'react';
import {
    Card, CardImg, CardBody, Row, Col, CardFooter, CardText
} from 'reactstrap';

export default function Collection(props) {
    return (
        <div>
            <Card>
                <CardImg top width="250px" src={props.images[0]} alt="main collection" />
                <CardBody className="ptb-1">
                    <Row>
                        {props.images.slice(1, 5).map((src, index) =>
                            (
                                <Col md={3} lg={3} key={index} className="pd-5">
                                    <CardImg height="70px" src={src} alt="other collection" />
                                </Col>
                            ))}
                    </Row>
                </CardBody>
                <CardFooter className="app-flex">
                    <CardText className="mb-0">{props.categoryName}</CardText>
                    <CardText className="pull-right mb-0">
                        <i className="rd__svg-icon">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                                <path d="M22 16V4c0-1.1-.9-2-2-2H8c-1.1 0-2 .9-2 2v12c0 1.1.9 2 2 2h12c1.1 0 2-.9 2-2zm-11-4l2.03 2.71L16 11l4 5H8l3-4zM2 6v14c0 1.1.9 2 2 2h14v-2H4V6H2z">
                                </path>
                            </svg>
                        </i>
                        {props.images.length}
                    </CardText>
                </CardFooter>
            </Card>
        </div>
    );
};