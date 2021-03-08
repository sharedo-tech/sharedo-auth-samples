import { reactive } from "vue";
import settings from "../settings";
import {get} from "./fetchWrapper";

// Observable/reactive profile info
export const profile = reactive({
    userId: null,
    name: null
});

/**
 * Load the current user profile info using the access token
 */
export function loadProfile()
{
    return get(`${settings.api}/api/security/userInfo`)
    .then(dto =>
        {
            profile.userId = dto.userId;
            profile.name = dto.fullName;
        });
};

