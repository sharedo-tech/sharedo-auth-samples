# ./client-creds.ps1 -Identity https://[your-identity-url] 
#                    -Api https://[your-sharedo-url]
#                    -ClientId [your-client-id] 
#                    -ClientSecret [your-client-secret]
Param
(
    [parameter(Mandatory=$true)] [string] $Identity,
    [parameter(Mandatory=$true)] [string] $Api,
    [parameter(Mandatory=$true)] [string] $ClientId,
    [parameter(Mandatory=$true)] [string] $ClientSecret
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
        "grant_type"="client_credentials"
        "scope"="sharedo"
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
# Note: The profile returned should be authenticated, but as it's
#       client credentials only, it will not contain any user info
#       and instead will list only the clientId and the permissions
#       granted directly to this client app. If your app to app
#       integration requires a user token, try the client credentials
#       with fixed impersonation or client credentials with dynamic
#       impersonation flows.
Get-Profile (Get-Token);





