import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";

import AppRouter from "./appRouter"

import store from "./model/store";

ReactDOM.render
    (
        <Provider store={store}>
            <AppRouter />
        </Provider>,
        document.getElementById("app-host")
    );
