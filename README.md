# Sharedo oAuth Samples

This repo contains a variety of common samples for using supported oAuth flows in order to interact with sharedo APIs.

Sharedo supports the following oAuth flows for obtaining access and refresh tokens, which can then be used to call sharedo APIs.

- Implicit
    - Typically used for single page web apps, or other clients that cannot keep a secret.
    - Simple to implement with one step authentication
    - Will only provide access tokens - renewal requires a redirect to the auth server
    - New browser policies concerning same site cookies, makes it difficult to renew access tokens seamlessly. (Normally, if a token expired, you would open a hidden iframe back to the auth server, which would immediately respond with a new access token. Same site cookie policy prevents this now, and the iframe would be deemed unauthenticated)
    - Consider using authorization code with proof key instead
    
- Authorization Code
    - Typically used for client applications that can keep a secret - e.g. custom web applications with a server back end.
    - Can be used with single page web apps and other clients that cannot keep a secret, but client secret should be considered to not be secret in those cases.
    - Two step authentication - authentication/consent, token exchange
    - Allows for access and refresh tokens to be issued
    - Consider using authorization code with proof key instead

- Hybrid
    - Mix between implicit flow and authorization code
    - Typically used for applications that can keep a secret
    - Can be used with single page web apps and other clients that cannot keep a secret, but client secret should be considered to not be a secret in those cases.
    - Two step authentication - authentication/consent, token exchange
    - Allows for access and refresh tokens to be issued
    - Consider using authorization code with proof key instead

- Client credentials    
    - Used for app to app integration between trusted services
    - Generally does not issue "user" tokens - app authentication only
        - Though there are two extended versions available in sharedo for obtaining impersonated user tokens
    - Does not require user authentication or consent (as it doesn't usually issue tokens for users)
    - Must never be used for single page web apps, or installable clients.

- Authorization Code with Proof Key (PKCE)
    - Can be used for applications that cannot keep a secret - e.g. SPAs, mobile applications, installable desktop clients etc.
    - As authorization code flow, but allows for dynamic client secrets to prove auth code responses come back from the authorisation server
    - Two step authentication - authentication/consent, token exchange
    - Allows for access and refresh tokens to be issued

## What does *can keep a secret* mean?
Many of the flows above require a client id and a mutually shared, pre-set secret phrase which should be kept secret and not leaked to users - valid ones or otherwise.

Server applications can keep a secret because the secret never leaves the server - the interface between the authorisation server and the client app is all done server side.

Client applications such as Javascript SPAs, mobile phone apps, or desktop applications are not considered to be able to keep a secret as that secret is easily discoverable. In a Javascript SPA, one can simply view source from the browser, in a binary application, it's often simple to open the compiled code to find stored strings like client secrets.

## General guidance on which flow to use?
You probably want authorization code with proof key, unless you're building a server based application (asp.net core MVC etc), in which case you probably want hybrid or authorization code flow.

## Tokens types
An *access token* is a digitally signed and trusted payload of information representing information about a client or user that is passed to API calls as the authorization header. Access tokens are usually quite short lived (~15 mins recommended for full JWT tokens, ~60 mins for reference tokens - see below).

A *refresh token* is a special token that can be stored by a client to re-authenticate itself to get new access tokens when they expire. A refresh token is linked to a user's account and can be revoked centrally.

For example - it's typical for mobile applications to require a user to sign into their service once, and then remember them from that point onwards until they sign out. Exiting the application and re-opening it will not generally require the user to login again, unless the app is for banking etc.

In those circumstances the client app with use auth code flow, or auth code with PKCE, to obtain an access token for immediate use, and a refresh token to get new ones later. Each time the client app calls the APIs, it uses the access token - if the token has expired, or on a first response of 401 Unauthorised, indicating the token has expired, it will then use the refresh token to obtain new tokens.

The server usually responds with a new access token, plus a new refresh token. The refresh token is long lived - usually on a sliding window of ~30-60 days. This means as long as the user opens the app at least once within that time frame, they won't have to sign in again.

The refresh token is linked to a user account within sharedo, allowing users to revoke the token at any time from sharedo itself.

*Important note on refresh tokens* - given a refresh token can be used to obtain access tokens and call APIs, the general guidance is that clients that cannot keep a secret should not store the reference token anywhere other than in memory. This should be adhered to where possible, but it is accepted that for a more optimal user experience, the refresh tokens will be persisted in clients like mobile or desktop applications. Refresh tokens should never be stored for javascript single page apps.

## Reference tokens
Within sharedo, there is a setting to allow a client to use "reference" tokens. These are not a separate type of token, but rather, are still access tokens but contain nothing more than a reference.

When reference tokens are turned off, the token issued is a JWT token. A signed JSON payload containing information about the user that can be read directly. When the JWT token is posed to the API server, it is fully trusted, as the content cannot be tampered with due to the digital signature (any changes to the content would invalidate the signature, thereby invalidating the token). 

This means the API server does not need to perform any additional validation on the token, saving a network call (+database lookups). 

Conversely to this convenience, it means that JWT tokens cannot be revoked. Once issued, they will remain valid and trusted until their expiry. For this reason, it's considered wise to give JWT access tokens a short expiry time - ~15 minutes.

Turning on reference tokens for a client will mean the authorisation server will not issue full JWT tokens. Instead, it will store the full JWT within the authorisation server and issue a reference code to it. This then means that the full JWT is not exposed, and the reference token can be revoked.

Concersely to this higher level of security, it means that reference tokens require an additional round trip to the authorisation server for every API call being authenticated with a reference token. The API will pass off the reference to the authorisation server, where it will validate and load the JWT from storage, then pass the full JWT back to the API.

It's recommended that client applications use reference tokens.

