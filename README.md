# Sharedo oAuth Samples

This repo contains a variety of common samples for using supported oAuth flows in order to interact with sharedo APIs.

Sharedo supports the following oAuth flows for obtaining access and refresh tokens, which can then be used to call sharedo APIs.

- Implicit
    - Will only provide access tokens
    - No longer recommended - remains in place for backward compatibility
    - Consider authorization code with proof key instead

- Authorization Code
    - Allows for access and refresh tokens to be issued
    - Provides for user authentication + obtaining access and refresh token on a back channel
    - Recommended for use in client applications that can keep a secret - e.g. custom web applications with a server back end (not SPAs)
    - Not recommended for single page web applications or installable clients
    - Consider authorization code with proof key instead

- Hybrid
    - Mix between implicit flow and authorization code
    - Allows for access and refresh tokens to be issued
    - Provides for user authentication + obtaining access and refresh token on a back channel
    - Recommended for use in client applications that can keep a secret - e.g. custom web applications with a server back end (not SPAs)
    - Not recommended for single page web applications or installable clients
    - Consider authorization code with proof key instead

- Client credentials    
    - Used for app to app integration between trusted services
    - Generally does not issue "user" tokens - app authentication only
        - Though there are two extended versions available in sharedo for obtaining impersonated user tokens
    - Does not require user authentication or consent (as it doesn't usually issue tokens for users)
    - Must never be used for single page web apps, or installable clients.

- Authorization Code with Proof Key (PKCE)
    - As authorization code flow, but allows for dynamic client secrets to prove auth code responses come back from the authorisation server
    - Allows for access and refresh tokens to be issued
    - Provides for user authentication + obtaining access and refresh tokens on a back channel
    - Recommended for use in any client application that can't keep a secret - e.g. SPAs, mobile applications, installable desktop applications etc.

## What does *can keep a secret* mean?
Many of the flows above require a client id and a mutually shared, pre-set secret phrase which should be kept secret and not leaked to users - valid ones or otherwise.

Server applications can keep a secret because the secret never leaves the server - the interface between the authorisation server and the client app is all done server side.

Client applications such as Javascript SPAs, mobile phone apps, or desktop applications are not considered to be able to keep a secret as that secret is easily discoverable. In a Javascript SPA, one can simply view source from the browser, in a binary application, it's often simple to open the compiled code to find stored strings like client secrets.

## General guidance on which flow to use?
You probably want authorization code with proof key, unless you're building a server based application (asp.net core MVC etc), in which case you probably want hybrid or authorization code flow.

