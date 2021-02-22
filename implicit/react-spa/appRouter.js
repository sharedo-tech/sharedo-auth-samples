import "core-js";
import React from "react";
import { connect } from "react-redux";
import { Switch, BrowserRouter, Route } from "react-router-dom";

import OAuthHandler from "./OAuthHandler";
import TopNav from "./components/TopNav";

import PageHome from "./modules/home/PageHome";
import PageTasks from "./modules/tasks/PageTasks";
import Error404 from "./modules/errorPages/Error404";

class MainAppRouter extends React.Component
{
    render()
    {
        return (
            <BrowserRouter>
                { !this.props.token && this.renderAuthRoutes()}
                { this.props.token && this.renderAppRoutes()}
            </BrowserRouter>
        );
    }

    renderAuthRoutes()
    {
        return (
            <>
                <Switch>
                    <Route component={OAuthHandler} />
                </Switch>
            </>
        )
    }

    renderAppRoutes()
    {
        return (
            <>
                <div className="sticky-top">
                    <TopNav />
                </div>
                <Switch>
                    <Route path="/" exact component={PageHome} />
                    <Route path="/tasks" exact component={PageTasks} />
                    <Route component={Error404} />
                </Switch>
            </>
        );
    }
}

const mapStateToProps = state => ({
    token: state.auth.token
});

export default connect(mapStateToProps, null)(MainAppRouter);