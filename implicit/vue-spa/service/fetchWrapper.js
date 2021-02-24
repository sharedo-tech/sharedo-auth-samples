import implicit from "./implicit";

function doFetch(url, request, resolve, reject)
{
    fetch(url, request).then(response =>
    {
        if (!response.ok)
        {
            reject(response.statusText);
            return;
        }

        resolve(response.json())
    }, err => 
    {
        reject(err);
    });
}

export function get(url)
{
    return new Promise((resolve, reject) =>
    {
        var token = implicit.auth.token;
        if (!token) reject("Not authorised");

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
        var token = implicit.auth.token;
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