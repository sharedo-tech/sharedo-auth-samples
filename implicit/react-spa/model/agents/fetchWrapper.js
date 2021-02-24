import store from "../store";

const getTokenFromStore = () =>
{
    if (store)
    {
        var state = store.getState();
        return state?.auth?.token;
    }

    return null;
}

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
        var token = getTokenFromStore();
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
        var token = getTokenFromStore();
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