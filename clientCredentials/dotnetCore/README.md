# .NET client credentials samples

There are three sample scripts in this folder.

## ClientCredentials
Uses the standard oAuth client-credentials flow to obtain an access token for calling sharedo APIs. The access token does not represent a user and can only be used to call APIs that do not require a user context.

To configure and run:
In sharedo administration, create a new app registration using the "client credentials" flow. Make a note of the client id and secret and run the sample, replacing the values in [square brackets] with your values;

```
dotnet run -Identity https://[your-identity-url] -Api https://[your-sharedo-url] -ClientId [your-client-id] -ClientSecret [your-client-secret]
```

## ClientCredentialsFixed
Uses an extended client-credentials flow to obtain an access token that impersonates a specific user when used to call sharedo APIs. This flow can be used to call any sharedo API for which the impersonated user has permission.

To configure and run:
In sharedo administration, create a new app registration using the "client credentials with fixed impersonation" flow. Set a user to be impersonated by this client app, make a note of the client id and secret and run the sample, replacing the values in [square brackets] with your values;

```
dotnet run -Identity https://[your-identity-url] -Api https://[your-sharedo-url] -ClientId [your-client-id] -ClientSecret [your-client-secret]
```

## ClientCredentialsDynamic
Uses an extended client-credentials flow to obtain an access token that impersonates a specified user when used to call sharedo APIs. This flow can be used to call any sharedo API for which the impersonated user has permission. Unlike the fixed impersonation above, where the client is registered with sharedo to impersonate a user, the client is registered for dynamic impersonation and the request to obtain tokens contains information about the user to be impersonated.

To configure and run:
In sharedo administration, create a new app registration using the "client credentials with dynamic impersonation" flow. Make a note of the client id and secret and run the sample, replacing the values in [square brackets] with your values;

```
dotnet run -Identity https://[your-identity-url] -Api https://[your-sharedo-url] -ClientId [your-client-id] -ClientSecret [your-client-secret] -UserIdentity [identity-of-user-to-impersonate] -UserIdentityProvider [identity-provider-of-user-to-impersonate]
```

The `[identity-of-user-to-impersonate]` should be set to the identity claim of a valid user on the system, along with setting the `[identity-provider-of-user-to-impersonate]` to the identity provider used to authenticate the user.