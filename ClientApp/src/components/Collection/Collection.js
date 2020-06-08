import React from "react";
import { useHistory } from "react-router-dom";
import {
  Card,
  CardImg,
  CardBody,
  Row,
  Col,
  CardFooter,
  CardText,
} from "reactstrap";
import { connect } from "react-redux";
import { setCollectData } from "../../creators/collectsCreator";
import "./_Collection.scss";

function Collection(props) {
  const [firstCollect, ...rest] = props.collectibles;
  const history = useHistory();
  const showCollects = (data, id) => {
    props.setCollectData(data);
    localStorage.setItem(`collect/${id}`, JSON.stringify(data));
    localStorage.setItem("collectionName", props.name);
    history.push(`/discover/${id}`);
  };
  return firstCollect ? (
    <div className="card__wrapper">
      <Card onClick={() => showCollects(props.collectibles, props.id)}>
        <div
          style={{
            maxWidth: "450px",
            backgroundImage: `url(${firstCollect.image.contentLower.afro()})`,
            backgroundSize: "cover",
            backgroundRepeat: "no-repeat",
            backgroundPosition: "center",
            borderTopLeftRadius: "10px",
            borderTopRightRadius: "10px",
          }}
        >
          <CardImg
            top
            src={firstCollect.image.contentLower}
            alt="main collection"
            style={{ visibility: "hidden" }}
          />
        </div>
        <CardBody className="ptb-1">
          <Row className="mx-0">
            {rest.map((collect, index) => {
              if (index < 4) {
                return (
                  <Col
                    md={3}
                    xs={3}
                    lg={3}
                    key={collect.id}
                    className="pd-5"
                    style={{
                      backgroundImage: `url(${collect.image.contentLower.afro()})`,
                      backgroundSize: "cover",
                      backgroundRepeat: "no-repeat",
                      backgroundPosition: "center",
                      maxHeight: "70px",
                    }}
                  >
                    <CardImg
                      style={{ visibility: "hidden" }}
                      src={collect.image.contentLower}
                      alt="other collection"
                    />
                  </Col>
                );
              }
            })}
          </Row>
        </CardBody>
        <CardFooter className="app-flex">
          <CardText className="mb-0">{props.name}</CardText>
          <CardText className="pull-right mb-0">
            <i className="rd__svg-icon">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path d="M22 16V4c0-1.1-.9-2-2-2H8c-1.1 0-2 .9-2 2v12c0 1.1.9 2 2 2h12c1.1 0 2-.9 2-2zm-11-4l2.03 2.71L16 11l4 5H8l3-4zM2 6v14c0 1.1.9 2 2 2h14v-2H4V6H2z"></path>
              </svg>
            </i>
            {props.collectibles.length}
          </CardText>
        </CardFooter>
      </Card>
    </div>
  ) : (
    <p>No collections yet</p>
  );
}

export default connect(null, { setCollectData })(Collection);
