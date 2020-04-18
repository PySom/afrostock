import React, { useState, useEffect } from 'react';
import SearchBar from '../SearchBar/SearchBar';
import ResultArea from '../SearchBar/ResultArea/ResultArea';
import VizSensor from 'react-visibility-sensor';
import { connect } from 'react-redux';

export function SearchArea({ className, setVisibleState, type }) {
    const [show, setShow] = useState(false);
    const [areaInView, setAreaInView] = useState(false);
    const [searchValue, setSearchValue] = useState("");


    const handleChange = (value) => {
        console.log(value)
        if (!show) setShow(true)
        setSearchValue(value);
    }

    useEffect(() => {
        if (searchValue && searchValue.length >= 3) {
            //api logic
        }

    }, [searchValue])


    const handleMouseOut = () => {
        setAreaInView(false)
    }

    const visualStateManager = (state) => {
        if (type === "head") {
            console.log("this new state", state)
            setVisibleState(state)
        }
    }

    return (
        <VizSensor onChange={(visible) => visualStateManager(visible)}>
            <div className={`${className ? className : ""}`}>
                <SearchBar searchValue={searchValue} setShow={setShow} show={show} onChange={handleChange} areaInView={areaInView} />
                <div className="m0-auto header-width r-p" onClick={() => setShow(true)}>
                    <ResultArea style={{ display: show ? "block" : "none" }}
                        onMouseOver={() => setAreaInView(true)} onMouseOut={handleMouseOut} />
                </div>
            </div>
        </VizSensor>
        
    )
}


//Below defines actions and reducers
const actionTypes = {
    visible: "SEARCH_VISBLE"
}

//actions
export const setVisibleState = (data) => ({ type: actionTypes.visible, data })

//reducers
export const visibleReducer = (state = true, action) => {
    switch (action.type) {
        case actionTypes.visible:
            console.log("new data is ", action.data)
            return action.data;
        default:
            return state;
    }
}

//get action from store in a connected form
export const ConnectedSearchArea = connect(null, { setVisibleState })(SearchArea)