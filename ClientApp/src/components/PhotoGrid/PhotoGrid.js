import React, { useState, useEffect } from 'react';
import "./PhotoGrid.css";
import getImages from '../../helper/imageHelper';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import ModalCustomHeader from '../Modal/ModalCustomHeader';
import ModalCustomBody from '../Modal/ModalCustomBody';

export default function PhotoGrid(props) {
    const [show, setShow] = useState(false);
    const [image, setImage] = useState(null);
    const [images, setImages] = useState([]);
    const [pageChanged] = useState(false)

    const handleClose = () => {
        setShow(false)
    }

    useEffect(() => {
        const images = getImages(25);
        setImages(images);

    }, [pageChanged])

    const handleShow = (e, index, src) => {
        e.preventDefault();
        setImage({src, index});
        setShow(true);
    }

    const adjustImage = (newIndex) => {
        console.log(newIndex)
        const lastIndex = images.length - 1;
        if (newIndex < 0) {
            newIndex = lastIndex;
        }
        else if (newIndex > images.length) {
            newIndex = 0;
        }
        setImage({ src: images[newIndex], index: newIndex })
    }

    return (
        <section id="photos">
            {images.map((src, index) => (
                <button key={index} onClick={(e) => handleShow(e, index, src)} className="unstyled mb-2">
                    <figure className="r-p">
                        <img src={src} alt="awesome cats" title="description of this image"/>
                        <h5 className="author-text f-12">Chisom Nwisu</h5>
                    </figure>
                    
                    
                </button>
            ))}

            <Modal isOpen={show} toggle={handleClose} className="photo-grid-modal">
                <ModalHeader toggle={handleClose}>
                    <ModalCustomHeader views={10} authorName="Chisom Nwisu" />
                </ModalHeader>
                <ModalBody>
                    <ModalCustomBody src={image && image.src} />
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
                    <Button color="secondary" onClick={handleClose}>Cancel</Button>
                </ModalFooter>
            </Modal>
        </section>
        )
}
