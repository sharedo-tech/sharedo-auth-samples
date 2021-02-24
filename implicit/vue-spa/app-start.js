import { createApp } from "vue";

import implicit from "./service/implicit";

import App from "./App.vue";


implicit.authorise().then(() =>
{
    const app = createApp(App);
    const vm = app.mount("#app-host");
});


