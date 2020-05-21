import React from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import Home from "./components/Home/Home";
import Discover from "./components/Discover/Discover";
import Contact from "./components/Contact";
import AboutUs from "./components/AboutUs";

import "./custom.css";
import "./scss/style.css";
import MainBody from "./components/MainBody/MainBody";
import VideoPage from "./components/VideoPage/VideoPage";
import Login from "./components/Login/Login";
import Register from "./components/Register/Register";
import Guard from "./Guard";
import Dashboard from "./components/Dashboard/Dashboard";
import Upload from "./components/Dashboard/Upload";
import EditProfile from "./components/Dashboard/EditProfile";

export default function App(props) {
  return (
    <Layout>
      <Route exact path="/" component={Home} />
      <Route
        exact
        path="/discover"
        render={() => (
          <MainBody>
            <Discover />
          </MainBody>
        )}
      />
      <Route
        exact
        path="/videos"
        render={() => (
          <Guard type="modal" route="videos">
            <MainBody>
              <VideoPage />
            </MainBody>
          </Guard>
        )}
      />
      <Route
        exact
        path="/dashboard"
        render={() => (
          <Guard type="modal" route="dashboard">
            <Dashboard />
          </Guard>
        )}
      />

      <Route
        exact
        path="/editprofile"
        render={() => (
          <Guard type="modal" route="editprofile">
            <EditProfile />
          </Guard>
        )}
      />

      <Route
        exact
        path="/upload"
        render={() => (
          <Guard type="modal" route="upload">
            <Upload />
          </Guard>
        )}
      />
      <Route
        exact
        path="/contents/:id"
        render={({ match }) => (
          <Guard type="modal" route={`/contents/${match.params.id}`}>
            <MainBody>
              <VideoPage match={match} />
            </MainBody>
          </Guard>
        )}
      />
      <Route exact path="/aboutUs" component={AboutUs} />
      <Route exact path="/contact" component={Contact} />
      <Route exact path="/login" component={Login} />
      <Route exact path="/register" component={Register} />
    </Layout>
  );
}

App.displayName = App.name;
