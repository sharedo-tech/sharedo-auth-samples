import 
{
    AUTH_TOKEN_RECEIVED, 
    AUTH_PROFILE_LOAD
} from "./actions";

import authAgent from "./agents/authAgent";

export function setToken(token)
{
    return { 
        type: AUTH_TOKEN_RECEIVED,
        payload: token
    };
}
window.st = setToken;
export function loadProfile()
{
    return dispatch =>
    {
        return authAgent.getProfile().then(r =>
        {
            dispatch({ type: AUTH_PROFILE_LOAD, payload: { name: r.fullName, userId: r.userId }});
        });
    }
}