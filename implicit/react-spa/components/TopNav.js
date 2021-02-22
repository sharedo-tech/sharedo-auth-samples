import React from "react";
import {connect} from "react-redux";
import { Link } from "react-router-dom";

import TopNavItem from "./TopNavItem";

import {openDrawer, closeDrawer} from "../model/uiActions"

class TopNav extends React.Component
{
    render()
    {
        var drawerClasses = "drawer";
        if( this.props.open ) drawerClasses += " open";

        return <>
            <nav className="navbar fixed">
                <div className="navbar-brand">
                    <span className="text-muted mr-sm mq-tablet-or-lower" onClick={ e => this.props.openDrawer()}>
                        <span className="fa fa-bars"></span>
                    </span>
                    <h1>
                        <Link to="/">
                        my<span className="text-primary">Sharedo</span>
                        </Link>
                    </h1>
                </div>
                <div className="navbar-group right">
                    {this.props.name || "Who are you?"}
                </div>
            </nav>
            <nav className={drawerClasses}>
                <div className="drawer-overlay" onClick={e => this.props.closeDrawer()}></div>
                <div className="drawer-content">
                    <div className="drawer-nav">
                        <ul>
                            <TopNavItem title="Main Menu"/>
                            <TopNavItem icon="fa-home" title="Home" to="/"/>
                            <TopNavItem icon="fa-list" title="Tasks" to="/tasks"/>
                        </ul>
                    </div>
                </div>
            </nav>
        </>;
    }
}

const mapStateToProps = (state, ownProps) => ({
    open: state.ui.drawerOpen,
    name: state.auth.name
});

const mapActionsToProps = ({
    openDrawer,
    closeDrawer
});

export default connect(mapStateToProps, mapActionsToProps)(TopNav);
