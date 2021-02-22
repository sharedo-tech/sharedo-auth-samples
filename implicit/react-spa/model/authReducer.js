import 
{
    AUTH_TOKEN_RECEIVED,
    AUTH_PROFILE_LOAD
} from "./authActions";

const initialState =
{
    token: null,
    name: null
};

export default function(state = initialState, action)
{
    switch(action.type)
    {
        case AUTH_TOKEN_RECEIVED:
            return {
                ... state,
                token: action.payload
            };

        case AUTH_PROFILE_LOAD:
            return {
                ... state,
                name: action.payload.name
            };

        default:
            return state;
    }
}