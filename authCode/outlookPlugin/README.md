# Outlook plugin with auth code flow

This is a simple VSTO plugin for outlook that demonstrates the use of auth code flow to authenticate a user, consent, and then call sharedo APIs.

In scenarios such as this, where the app is distributed as an installable product, it would be more ideal to use auth code with PKCE as the flow, as that would not involve client secrets being shipped in any binary asset. However, given auth code flow requires user sign in and consent, it is acceptable to consider client secrets in these scenarios to be non secret.

To configure and run:
In sharedo administration, create a new app registration using the "authorization code" flow. 

Set the client id and the client secret - make a note of both, remembering that the secret is, in this scenario not considered to be truly secret. Set the access token lifetime to 900 (15 minutes) and the refresh token lifetime to 2592000 (about a month) and ensure that use reference tokens is turned on.

Then, add a reply url for `oob://localhost/my-outlook` - the VSTO add in will detect replies to this address after authentication and extract the authorisation code, which it then exchanges for access and refresh tokens.

Ensure "require user consent" is switched on and specify which identity providers are valid for sign in using this app.

With the sharedo app client registration complete, you can edit the settings in `App.cs` in the project `Sharedo.OutlookSample`.

1. Change the `ClientId` setting to the client id you created in sharedo.
2. Change the `ClientSecret` setting to the client secret you created in sharedo.
3. Press F5 to run the project and install the add in.

## Using the app
TODO: Add more detail....

- New ribbon
- Select configure, enter identity URL and sharedo API URL
- Select link account, follow the auth prompts
- Once linked, the debug ribbon item will appear, which will list info about the user the token is associated with, plus a list of tasks the user has access to.
