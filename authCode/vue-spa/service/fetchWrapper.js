import { auth, redirectToLogin, getNewTokens } from "./authcode";

function fetchAttempt(url, request, resolve, reject, attempt)
{
    fetch(url, request).then(
        response =>
        {
            if (!response.ok)
            {
                if (response.status === 401)
                {
                    if (attempt === 1)
                    {
                        console.warn("First attempt 401 at " + url + " will try to obtain new tokens");
                        getNewTokens().then(
                            newAccessToken =>
                            {
                                console.log("Retrying " + url);
                                request.headers["Authorization"] = `Bearer ${newAccessToken}`;
                                fetchAttempt(url, request, resolve, reject, attempt + 1);
                            },
                            () =>
                            {
                                console.warn("Could not obtain new tokens, redirecting to login");
                                //redirectToLogin();
                            });
                        return;
                    }
                    else
                    {
                        console.warn("Attempt " + attempt + " 401 at " + url + ", redirecting to login");
                        //redirectToLogin();
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

function doFetch(url, request, resolve, reject)
{
    return fetchAttempt(url, request, resolve, reject, 1);
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