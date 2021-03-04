import { reactive } from "vue";
import router from "../router";
import settings from "../settings";

export const auth = reactive({
    accessToken: null,
    refreshToken: null
});

export function redirectToLogin()
{
    // Get the requested path from the url, we'll restore it later
    var state = window.location.pathname;

    var url = settings.identity;
    url += "/connect/authorize";
    url += `?client_id=${settings.clientId}`;
    url += "&scope=sharedo offline_access";
    url += "&response_type=code"
    url += `&redirect_uri=${encodeURIComponent(settings.redirectUri)}`;
    url += `&state=${encodeURIComponent(state)}`;

    window.location.href = url;
};

function getTokensFromStorage()
{
    var at = localStorage.getItem("_at");
    var rt = localStorage.getItem("_rt");
    if( at ) auth.accessToken = at;
    if( rt ) auth.refreshToken = rt;
}

export function clearTokens()
{
    updateTokens(null, null);
}

export function updateTokens(access, refresh)
{
    if( access )
    {
        localStorage.setItem("_at", access);
        auth.accessToken = access;
    }
    else
    {
        localStorage.removeItem("_at");
        auth.accessToken = null;
    }

    if( refresh )
    {
        localStorage.setItem("_rt", refresh);
        auth.refreshToken = refresh;
    }
    else
    {
        localStorage.removeItem("_rt");
        auth.refreshToken = null;
    }
}

export function logout()
{
    // TODO: call the token endpoint to invalidate the refresh token
    clearTokens();
    return Promise.resolve();
}

window.logout = logout;

function getQueryParams()
{
    var url = window.location.href;
    var result = {};
    var params = url.substring(url.indexOf("?") + 1).split("&");

    params.forEach(v =>
    {
        var kvp = v.split("=");
        result[kvp[0]] = decodeURIComponent(kvp[1]);
    });

    return result;
}

function receiveCode()
{
    return new Promise((resolve, reject) =>
    {
        // Is there a code and state parameter in the query string
        var q = getQueryParams();
        if (!q.code)
        {
            reject("No code presented in oAuth response");
            return;
        }

        // Exchange code for access and refresh tokens
        var url = `${settings.identity}/connect/token`;
        var request =
        {
            method: "POST",
            cache: "no-cache",
            headers:
            {
                "Accept": "application/json",
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: "client_id=" + settings.clientId +
                "&client_secret=" + settings.clientSecret +
                "&grant_type=authorization_code" +
                "&scope=sharedo+offline_access" +
                "&redirect_uri=" + encodeURIComponent(settings.redirectUri) +
                "&code=" + q.code
        };

        fetch(url, request).then(response =>
        {
            if (!response.ok)
            {
                reject(response.status + " Could not exchange auth code" + response.statusText);
                return;
            }

            response.json().then(data =>
            {
                updateTokens(data["access_token"], data["refresh_token"]);
                router.setPath(decodeURIComponent(q.state || "/"), true);
                resolve();
            });
        }, err => 
        {
            reject(err);
        });
    });
};

export function initialise()
{
    return new Promise((resolve, reject) =>
    {
        // Are we receiving a code response?
        if (window.location.pathname.toLowerCase() === "/oauthreply")
        {
            resolve(receiveCode());
            return;
        };

        // No - check existing tokens and redirect if necessary
        // Do we have a token in storage?
        getTokensFromStorage();
        if( auth.accessToken && auth.refreshToken )
        {
            resolve();
            return;
        }

        // Not receiving and nothing stored - go login please.
        redirectToLogin();
    });
}


