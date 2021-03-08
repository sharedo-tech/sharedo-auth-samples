import { reactive } from "vue";
import settings from "../settings";
import {get} from "./fetchWrapper";

export const profile = reactive({
    userId: null,
    name: null
});


export function loadProfile()
{
    return get(`${settings.api}/api/security/userInfo`)
    .then(dto =>
        {
            profile.userId = dto.userId;
            profile.name = dto.fullName;
        });
};

