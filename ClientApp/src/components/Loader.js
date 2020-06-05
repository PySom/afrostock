import React from "react";
import Loader from "react-loader-spinner";
import { setLoader } from "../creators/loaderCreator";
import { connect } from "react-redux";
class Loadar extends React.Component {
  constructor(props) {
    super(props);
  }

  timer() {
    if (this.props.loadar) {
      return 2000;
    } else {
      return 2000;
    }
  }

  //other logic
  render() {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "80vh",
        }}
      >
        <Loader
          type="Puff"
          color="#966b4f"
          height={200}
          width={200}
          timeout={10000} //3 secs
        />
      </div>
    );
  }
}

const mapStateToProps = ({ loadar }) => {
  return { loadar };
};

export default connect(mapStateToProps, { setLoader })(Loadar);
