# Auth code flow samples

This folder contains samples for various languages that will utilise the oAuth auth code flow to obtain an access token and refresh token (used to obtain new access tokens).

This flow allows for linking a 3rd party app to sharedo APIs in a persistent way - much like how apps for mobile ask the user to sign in once at first start up, and then don't require login again. They achieve this by storing a refresh token, which is then used to periodically obtain new access tokens to call the API. So long as the app is used within a sliding window, the refresh token remains valid.

