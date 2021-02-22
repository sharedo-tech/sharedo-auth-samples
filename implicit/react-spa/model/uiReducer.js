import
{
    UI_DRAWER_OPEN,
    UI_DRAWER_CLOSE
} from "./uiActions";

const initialState = {
    drawerOpen: false
};

export default function(state = initialState, action)
{
    switch( action.type )
    {
        case UI_DRAWER_OPEN:
            return {
                ...state,
                drawerOpen: true
            };
        case UI_DRAWER_CLOSE:
            return {
                ...state,
                drawerOpen: false
            };
        default:
            return state;
    }
}