import React, { useState } from 'react';
import ls from './sideEffects/local/ourLocalStorage'
import { Redirect } from 'react-router-dom';
import { Modal, ModalBody, ModalHeader } from 'reactstrap';
import Login from './components/Login/Login';
import UnAthorized from './components/UnAthorized/UnAuthorized';

export default function Guard({ type, route, children, callback, admin }) {
    //get user from store
    const [show, setShow] = useState(true);
    const user = ls.getItemInLs("user");
    return (
        !user
            ?
            <>
                {
                    type === "route" &&
                    <Redirect to={`/login?returnurl=${route}`} />
                }
                {
                    type === "modal" &&
                    <Modal isOpen={show}>
                        <ModalHeader toggle={() => setShow(false)}>
                        </ModalHeader>
                        <ModalBody>
                            <Login type="modal" route={route} callback={callback} />
                        </ModalBody>
                        
                    </Modal>
                }
            </>
            :
            admin
                ?
                user.isdmin ? 
                    <>{children}</>
                    :
                    <UnAthorized />
                :
                <>{children}</>

    )
}