import React from "react";
import {connect} from "react-redux";

class PageHome extends React.Component
{
    render()
    {
        return (
            <div className="module module-page">
                <h1 className="mb-1">Home</h1>
                <div>Welcome to my-sharedo {this.props.name}</div>
            </div>
        );
    }
}

const mapStateToProps = (state, ownProps) => ({
    name: state.auth.name
});

const mapActionsToProps = ({
});

export default connect(mapStateToProps, mapActionsToProps)(PageHome);
