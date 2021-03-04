import { createApp } from "vue";

import {initialise} from "./service/authcode";

import App from "./App.vue";

initialise().then(() =>
{
    // TODO: Load profile
    const app = createApp(App);
    const vm = app.mount("#app-host");
});


