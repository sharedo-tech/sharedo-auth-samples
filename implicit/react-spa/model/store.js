import {createStore, applyMiddleware, combineReducers} from "redux";
import thunk from "redux-thunk";

import authReducer from "./authReducer";
import uiReducer from "./uiReducer";

const initialState = {};
const middleware = [thunk];

const rootReducer = combineReducers({
    auth: authReducer,
    ui: uiReducer
});

const store = createStore
(
    rootReducer,
    initialState,
    applyMiddleware(... middleware)
);

export default store;
