import React from "react";
import "./_Contact.scss";
import Footer from "./Footer";

export default function Contact() {
  return (
    <div className="contact__wrapper">
      <div className="enforce-mp">
        <div>
          <h2 className="question">
            Do you have a specific brief in mind? <br /> Or do you need help
            buying images?
          </h2>
          <h4 className="email">
            Send us an email at{" "}
            <a href="mailto:help@afrostockstudio.com">
              help@afrostockstudio.com
            </a>{" "}
            and we’ll get back to you within 24 hours (except at the weekends).
          </h4>
        </div>
        <div className="address">
          <span>Head Office Afrostockstudio Limited</span>
          <span>28 UNIBEN Close Brains & Hammers Estate, </span>
          <span>Galadimawa, Abuja FCT </span>
          <span>
            <a href="tel:+234 09010010098">+234 09010010098</a>
          </span>
        </div>
      </div>
      <Footer></Footer>
    </div>
  );
}
