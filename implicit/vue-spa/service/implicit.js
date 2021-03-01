import { reactive } from "vue";
import settings from "../settings";
import { get } from "./fetchWrapper";
import router from "../router";

var auth = reactive({
    token: null,
    name: null,
    userId: null
});

function redirectToLogin()
{
    // Get the requested path from the url, we'll restore it later
    var state = window.location.pathname;

    var url = settings.identity;
    url += "/connect/authorize";
    url += `?client_id=${settings.clientId}`;
    url += "&scope=sharedo";
    url += "&response_type=token"
    url += `&redirect_uri=${encodeURIComponent(settings.redirectUri)}`;
    url += `&state=${encodeURIComponent(state)}`;

    window.location.href = url;
};

function receiveOAuthResponse()
{
    // Is there an oAuth response in the hash?
    var q = window.location.hash.substring(1);
    if (!q) return false;

    var token = null;
    var redirect = "/";

    var pairs = q.split("&");
    for (var i = 0; i < pairs.length; i++)
    {
        var kvp = pairs[i].split("=");
        if (kvp.length < 2) continue;

        if (kvp[0] === "access_token") token = kvp[1];
        if (kvp[0] === "state") redirect = kvp[1];
    }

    if (!token) return false;

    auth.token = token;
    router.setPath(decodeURIComponent(redirect), true);
    return true;
};

function authorise()
{
    return new Promise((resolve, reject) =>
    {
        if(receiveOAuthResponse())
        {
            // Received a new token - load user profile
            get(`${settings.api}/api/security/userInfo`).then(profile =>
            {
                auth.name = profile.fullName;
                auth.userId = profile.userId;
                resolve();
            });

            return;
        }

        redirectToLogin();
    });
};

export default {
    auth,
    authorise,
    redirectToLogin
}
