import settings from "../../settings";
import {get} from "./fetchWrapper";

function getProfile()
{
    return get(`${settings.api}/api/security/userInfo`);
}

export default 
{
    getProfile
};
