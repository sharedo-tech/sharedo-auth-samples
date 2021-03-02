# Implicit flow samples

This folder contains samples for various languages that will utilise the oAuth implicit flow to obtain an access token (and only an access token).

This flow is designed for app integration with clients that cannot keep a secret. It's limited in the fact that the access token will be valid for a period of time and cannot be renewed without redirecting the user back to the auth server.

Historically, to keep the access token valid for a session, the app would open a hidden iframe that would periodically navigate to the auth server. As the auth server would already have a valid session for the user, it would typically send a new access code back and the session could be kept alive that way.

However, with the advent of strict enforcement of same site cookies, this approach is no longer valid. When the iframe opens to the auth server, it will refuse to send any cookies, meaning it would not have a valid session for the user and would not return new access codes.

In the vuejs SPA example, this is partially addressed - whenever the API call receives a 401 unauthorised response from the API, indicating the token has expired, it fully redirects the browser to the auth server, which, not being a cross origin iframe etc, will maintain session and return new tokens.

That can hardly be considered robust handling though given the API call that caused the redirect will not have been retried once a new access token is returned, however it serves as a reasonable example of using the simple implicit flow.

For more robust handling, consider using authorization code flow or authorization code with PKCE.
