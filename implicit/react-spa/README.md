# React SPA

This is a simple react SPA type application using implicit flow.

To configure and run:
In sharedo administration, create a new app registration using the "implicit" flow. Make sure to set `http://localhost:8200/oAuthReply` as a valid redirect url, and also add `http://localhost:8200` as a valid CORS origin. Make a note of the client id.

1. Edit settings.js
Update settings.js to reflect your test sharedo instance.

```
var settings =
{
    identity: "https://[your-identity-url]",
    api: "https://[your-sharedo-url]",
    clientId: "[your-clientid]",
    redirectUri: "http://localhost:8200/oAuthReply"
}
```

As an alternative you can also set the following environment variables, provided you're running the sample using `npm`;

```
$env:IDENTITY="https://[your-identity-url]"
$env:API="https://[your-sharedo-url]"
$env:CLIENTID="[your-clientid]"
```

2. Restore packages
`npm install`

3. Run it
`npm run dev`

4. Browse it
Open your browser at [http://localhost:8200] - you should be redirected to sharedo to sign in and consent to let the app access data, then returned to the sample.
