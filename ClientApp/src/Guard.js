import React, { useState } from "react";
import ls from "./sideEffects/local/ourLocalStorage";
import { Redirect } from "react-router-dom";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import Login from "./components/Login/Login";
import UnAthorized from "./components/UnAthorized/UnAuthorized";

export default function Guard({ type, route, children, callback, closeWhenDone, admin }) {
  //get user from store
    const [show, setShow] = useState(true);
    const user = ls.getUserInLs();
    console.log(({user}))
  return !user ? (
    <>
      {type === "route" && <Redirect to={`/login?returnurl=${route}`} />}
      {type === "modal" && (
        <Modal isOpen={show}>
          <ModalHeader toggle={() => setShow(false)}></ModalHeader>
          <ModalBody>
                      <Login type="modal" route={route} callback={callback} closeMe={closeWhenDone && (() => setShow(false))} />
          </ModalBody>
        </Modal>
      )}
    </>
  ) : admin ? (
    user.role === "Super" ? (
      <>{children}</>
    ) : (
      <UnAthorized />
    )
  ) : (
    <>{children}</>
  );
}
