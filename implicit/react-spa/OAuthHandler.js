import React from "react";
import { connect } from "react-redux";

import settings from "./settings";
import {setToken, loadProfile} from "./model/authActions";

class OAuthHandler extends React.Component
{
    componentDidMount()
    {
        // Do we have an oauth response and redirect?
        var redirect = this.processOAuthResponse();
        if( redirect )
        {
            this.props.history.replace(redirect)
            return;
        }

        // No token, no response - send user to auth
        this.redirectToLogin();
    }

    redirectToLogin()
    {
        var state = window.location.pathname;

        var url = settings.identity;
        url += "/connect/authorize";
        url += `?client_id=${settings.clientId}`;
        url += "&scope=sharedo";
        url += "&response_type=token"
        url += `&redirect_uri=${encodeURIComponent(settings.redirectUri)}`;
        url += `&state=${encodeURIComponent(state)}`;
        url += ``
        window.location.href = url;
    }

    processOAuthResponse()
    {
        // Is there an oAuth response in the hash?
        var q = window.location.hash.substring(1);
        if( !q ) return null;

        var token = null;
        var redirect = "/";

        var pairs = q.split("&");
        for(var i=0; i<pairs.length; i++)
        {
            var kvp = pairs[i].split("=");
            if( kvp.length < 2 ) continue;

            if( kvp[0] === "access_token" ) token = kvp[1];
            if( kvp[0] === "state") redirect = kvp[1];
        }
    
        if( !token ) return null;

        this.props.setToken(token);
        this.props.loadProfile();
        
        return decodeURIComponent(redirect);
    }

    render()
    {
        return null;
    }
}

const mapStateToProps = (state, ownProps) => ({
    history: ownProps.history
});

const mapDispatchToProps = {
    setToken,
    loadProfile
};

export default connect(mapStateToProps, mapDispatchToProps)(OAuthHandler);