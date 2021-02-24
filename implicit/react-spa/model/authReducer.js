import 
{
    AUTH_TOKEN_RECEIVED,
    AUTH_PROFILE_LOAD
} from "./actions";

const initialState =
{
    token: null,
    name: null,
    userId: null
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
                name: action.payload.name,
                userId: action.payload.userId
            };

        default:
            return state;
    }
}