import React, { useState } from 'react';
import "./PhotoGrid.css";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import ModalCustomHeader from '../Modal/ModalCustomHeader';
import ModalCustomBody from '../Modal/ModalCustomBody';
import RavePay from '../RavePay/RavePay';
import api from '../../sideEffects/apis/api';
import Guard from '../../Guard';

export default function PhotoGrid({ contents }) {
    const [show, setShow] = useState(false);
    const [showRaveButton, setShowRaveButton] = useState(false);
    const [image, setImage] = useState(null);
    console.log({contents})

    const handleClose = () => {
        setShow(false)
    }

    const handleShow = (e, index, src) => {
        e.preventDefault();
        api.updateWithId("images/increaseview/" + src.id)
            .then(res => console.log(res))
            .catch(err => console.log(err))
        setImage({src, index});
        setShow(true);
    }

    const adjustImage = (newIndex) => {
        if (contents.length) {
            const lastIndex = contents.length - 1;
            if (newIndex < 0) {
                newIndex = lastIndex;
            }
            else if (newIndex > contents.length) {
                newIndex = 0;
            }
            setImage({ src: contents[newIndex], index: newIndex })
        }
        
    }

    function appropriateClass(orientation){
        switch (orientation) {
            case 0:
                return ""
            case 1:
                return "portrait"
            case 2:
                return "landscape"
            case 3:
                return "big"
            default:
                return ""
        }
    }

    console.log({ showRaveButton })

    return (
        <section id="photos">
            {contents
                && 
                contents.map((content, index) => (
                    <button key={content.id} onClick={(e) => handleShow(e, index, content)} className={`unstyled mb-2 ${appropriateClass(content.orientation)}`}>
                        {
                            content.contentType === 0 &&
                            <figure className="r-p">
                                <img src={content.content} alt={content.name} title={content.description || content.name} />
                                <h5 className="author-text text-title f-12">{`${content.author.firstName} ${content.author.surName}`}</h5>
                            </figure>
                        }

                        {
                            content.contentType === 1 &&
                            <div className="r-p" >
                                <video controls src={content.content}>
                                </video>
                                <h5 className="author-text text-title f-12">{`${content.author.firstName} ${content.author.surName}`}</h5>
                            </div>

                        }

                    </button>
                ))
            }

            {
                image &&
                <Modal isOpen={show} toggle={handleClose} className="photo-grid-modal">
                    <ModalHeader toggle={handleClose}>
                        <ModalCustomHeader views={image.src.views} onClick={() => setShowRaveButton(true)}
                            authorName={image.src.author.surName + " " + image.src.author.firstName } />
                    </ModalHeader>
                    <ModalBody>
                        <ModalCustomBody tags={image.src.tags} name={image.name}
                            description={image.src.description} contentType={image.src.contentType}
                            src={image.src.content} />
                    </ModalBody>
                    <ModalFooter>
                        <div className="app-flex flex-ceter">
                            <button onClick={() => adjustImage(image.index - 1)} className="move-left unstyled">
                                &lt;
                        </button>
                            <button onClick={() => adjustImage(image.index + 1)} className="move-left unstyled">
                                &gt;
                        </button>
                        </div>
                        {
                            showRaveButton &&
                            <Guard type="modal" closeWhenDone={true} toggle={() => setShowRaveButton(false)}>
                                <RavePay customer_email="gmangeorge@ymail.com" customer_phone="08038714611" amount={image.src.amount} src={image.src.content} />
                            </Guard>
                            
                        }
                        <Button color="secondary" onClick={handleClose}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            }
            
        </section>
        )
}
