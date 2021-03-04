
var settings =
{
    identity: "https://[your-identity-url]",
    api: "https://[your-sharedo-url]",
    clientId: "[your-clientid]",
    clientSecret: "[your-clientsecret]",
    redirectUri: "http://localhost:8200/oAuthReply"
}

// Default to environment variables if specified
// Set from command line before launching `npm run dev` or `npm run build`
// e.g.: $env:IDENTITY="https://my-sharedo-identity.local"
//       $env:API="https://my-sharedo.local"
//       $env:CLIENTID="my-id"
// 
// This is here so we don't commit URLs or client id's to git.
//
// You can remove this if you've set the settings up above
// 
if( process && process.env )
{
    settings.identity = process.env.IDENTITY || settings.identity;
    settings.api = process.env.API || settings.api;
    settings.clientId = process.env.CLIENTID || settings.clientId;
    settings.clientSecret = process.env.CLIENTSECRET || settings.clientSecret;
}

export default settings;
