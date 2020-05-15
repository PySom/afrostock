import React, { useState } from 'react';
import { Route } from 'react-router';
import { useRouteMatch } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home/Home';
import Discover from './components/Discover/Discover';
import Contact from './components/Contact';
import AboutUs from './components/AboutUs';

import './custom.css'
import MainBody from './components/MainBody/MainBody';
import VideoPage from './components/VideoPage/VideoPage';
import api from './sideEffects/apis/api';
import Guard from './Guard';
import Register from './components/Register/Register';
import Login from './components/Login/Login';

export default function App(props) {
    
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/discover' render={() => (
                <MainBody>
                    <Discover />
                </MainBody>
                )
            } />
            <Route exact path='/videos'
                render={() =>
                    (
                        <MainBody>
                            <VideoPage />
                        </MainBody>
                    )
                } />
            <Route exact path='/contents/:id'
                render={({ match }) =>
                    (
                        <Guard type="modal" route={`/contents/${match.params.id}`}>
                            <MainBody>
                                <VideoPage match={match} />
                            </MainBody>
                        </Guard>
                        
                    )
                } />
            <Route exact path='/aboutUs' component={AboutUs} />
            <Route exact path='/contact' component={Contact} /
            ><Route exact path='/register' component={Register} />
            <Route exact path='/login' component={Login} />
        </Layout>
    );
}


App.displayName = App.name;