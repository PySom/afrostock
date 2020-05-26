import React from "react";
import Rave from "react-flutterwave-rave";
import "./_Rave.scss";

export default function RavePay({
  customer_email,
  customer_phone,
  amount,
  src,
}) {
  const callback = (response) => {
    console.log(response);
    const fileName = src.split("\\");
    const tag = document.createElement("a");
    tag.href = src;
    tag.download = fileName[1];
    document.body.appendChild(tag);
    tag.click();
    document.body.removeChild(tag);
  };
  const onclose = () => {
    console.log("user closed");
  };
  const raveProps = {
    pay_button_text: "Pay With Rave",
    class: "rave__button",
    payment_method: "card",
    customer_email,
    customer_phone,
    amount: "" + amount + "",
    ravePubKey: "FLWPUBK-1d4c9f3f90d9b73a0ca5a18852cd04df-X",
    callback,
    onclose,
  };
  return (
    <div className="App">
      <Rave {...raveProps} />
    </div>
  );
}
