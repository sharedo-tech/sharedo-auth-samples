# ./client-creds-dynamic.ps1 -Identity https://[your-identity-url] 
#                            -Api https://[your-sharedo-url] 
#                            -ClientId [your-client-id] 
#                            -ClientSecret [your-client-secret] 
#                            -UserIdentity [identity-of-user-to-impersonate] 
#                            -UserIdentityProvider [identity-provider-of-user-to-impersonate]
Param
(
    [parameter(Mandatory=$true)] [string] $Identity,
    [parameter(Mandatory=$true)] [string] $Api,
    [parameter(Mandatory=$true)] [string] $ClientId,
    [parameter(Mandatory=$true)] [string] $ClientSecret,
    [parameter(Mandatory=$true)] [string] $UserIdentity,
    [parameter(Mandatory=$true)] [string] $UserIdentityProvider
);

$ErrorActionPreference = "Stop"

# Gets a token from the identity service
function Get-Token
{
    $auth = ($ClientId + ":" + $ClientSecret);
    $authBytes = [System.Text.Encoding]::UTF8.GetBytes($auth);
    $auth = [Convert]::ToBase64String($authBytes);
    $token = $null

    $headers = @{};
    $headers["accept"] = "application/json";
    $headers["Content-Type"] = "application/x-www-form-urlencoded"
    $headers["Authorization"] = "Basic $auth";

    $body = @{
        "grant_type"="Impersonate.Specified"
        "scope"="sharedo"
        "impersonate_user"=$UserIdentity
        "impersonate_provider"=$UserIdentityProvider
    };

    $response = invoke-webrequest `
        -Uri $Identity/connect/token `
        -Method Post `
        -Body $Body `
        -ContentType $ContentType `
        -Headers $headers `
        -UseBasicParsing;

    $data = ConvertFrom-Json($response.Content);
    return $data.access_token;
}

# Calls the sharedo user introspection API to get information
# about the current user/client application.
function Get-Profile
{
    Param([parameter(Mandatory=$true)] [string] $token);

    $headers = @{};
    $headers["accept"] = "application/json";
    $headers["Authorization"] = "Bearer $token";

    $response = invoke-webrequest `
        -Uri $Api/api/security/userInfo `
        -Method Get `
        -Headers $headers `
        -UseBasicParsing;

    $data = ConvertFrom-Json($response.Content);
    return $data;
}

# Entry point
# Note: Unlike standard client credentials flow, which authenticates
#       only the client app itself and does not return a token that
#       represents a user, the fixed impersonation flow will return
#       a token that impersonates the user configured in the app
#       registration.
Get-Profile (Get-Token);





