import React from "react";
import ls from "./sideEffects/local/ourLocalStorage";
import { Redirect } from "react-router-dom";
import { Modal, ModalBody } from "reactstrap";
import Login from "./components/Login/Login";

export default function Guard({ type, route, children }) {
  //get user from store
  const user = ls.getItemInLs("user");
  return !user ? (
    <>
      {type === "route" && <Redirect to={`/login/returnurl=${route}`} />}
      {type === "modal" && (
        <Modal show={true}>
          <ModalBody>
            <Login type="modal" />
          </ModalBody>
        </Modal>
      )}
    </>
  ) : (
    <>{children}</>
  );
}
