import React from "react";
import {connect} from "react-redux";
import { Link } from "react-router-dom";
import {closeDrawer} from "../model/uiActions";

class TopNavItem extends React.Component
{
    render()
    {
        if( this.props.to )
        {
            return (
                <li className="link">
                    <Link to={this.props.to} onClick={() => this.props.closeDrawer() }>
                        {this.props.icon &&
                         <span className={`fa fa-fw ${this.props.icon}`}></span>
                        }
                        {this.props.title}
                    </Link>
                </li>
            );
        }

        return <li className="title">{this.props.title}</li>
    }
}

TopNavItem.defaultProps =
{
    icon: null,
    title: null,
    to: null
};

const mapStateToProps = (state, ownProps) => ({
    icon: ownProps.icon,
    title: ownProps.title,
    to: ownProps.to
});

const mapActionsToProps = ({
    closeDrawer
});

export default connect(mapStateToProps, mapActionsToProps)(TopNavItem);