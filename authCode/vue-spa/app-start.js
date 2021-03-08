import { createApp } from "vue";

import {initialise} from "./service/authcode";
import {loadProfile} from "./service/profile";

import App from "./App.vue";

initialise().then(() =>
{
    loadProfile().then(() =>
    {
        const app = createApp(App);
        const vm = app.mount("#app-host");
    });
});


