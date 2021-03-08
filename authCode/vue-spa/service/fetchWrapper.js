import { auth, redirectToLogin, getNewTokens } from "./authcode";

/**
 * Fetch wrapper that will also manage access tokens. If it receives a 401 unauthorised
 * response when calling the API, it will, first try to obtain new access tokens via the refresh token,
 * and, if successful, repeat the API call. If it can't renew, or it 401's again, then user is redirected
 * back to sign in as their tokens are now completely invalid.
 */
function doFetch(url, request, resolve, reject, attempt)
{
    if( !attempt ) attempt = 1;

    // Do the request
    return fetch(url, request).then(
        response =>
        {
            if (!response.ok)
            {
                if (response.status === 401)
                {
                    // Handle first chance 401 - renew tokens.
                    if (attempt === 1)
                    {
                        console.warn("First attempt 401 at " + url + " will try to obtain new tokens");
                        getNewTokens().then(
                            newAccessToken =>
                            {
                                console.log("Retrying " + url);
                                request.headers["Authorization"] = `Bearer ${newAccessToken}`;
                                doFetch(url, request, resolve, reject, attempt + 1);
                            },
                            () =>
                            {
                                console.warn("Could not obtain new tokens, redirecting to login");
                                redirectToLogin();
                            });
                        return;
                    }
                    // Handle 2nd chance 401 - fail and redirect
                    else
                    {
                        console.warn("Attempt " + attempt + " 401 at " + url + ", redirecting to login");
                        redirectToLogin();
                        return;
                    }
                }

                // Other status - reject outright
                reject(response.statusText);
                return;
            }

            resolve(response.json())
        },
        err => 
        {
            reject(err);
        });
}

export function get(url)
{
    return new Promise((resolve, reject) =>
    {
        var token = auth.accessToken;
        if (!token) reject();

        var request =
        {
            method: "GET",
            cache: "no-cache",
            headers:
            {
                "Authorization": `Bearer ${token}`,
                "Accept": "application/json"
            }
        };

        doFetch(url, request, resolve, reject);
    });
}

export function post(url, body)
{
    return new Promise((resolve, reject) =>
    {
        var token = auth.accessToken;
        if (!token) reject("Not authorised");

        var request =
        {
            method: "POST",
            cache: "no-cache",
            body: JSON.stringify(body),
            headers:
            {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`,
                "Accept": "application/json"
            }
        };

        doFetch(url, request, resolve, reject);
    });
}