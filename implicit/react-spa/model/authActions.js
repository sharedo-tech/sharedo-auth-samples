
export const AUTH_TOKEN_RECEIVED = "AUTH_TOKEN_RECEIVED";
export const AUTH_PROFILE_LOAD = "AUTH_PROFILE_LOAD";

export function setToken(token)
{
    return { 
        type: AUTH_TOKEN_RECEIVED,
        payload: token
    };
}

export function loadProfile()
{
    return dispatch =>
    {
        return new Promise((resolve, reject) =>
        {
            setTimeout(() =>
            {
                dispatch({ type: AUTH_PROFILE_LOAD, payload: { name: "TODO: Load profile" } });
                resolve();
            }, 5000);
        });
    }
}