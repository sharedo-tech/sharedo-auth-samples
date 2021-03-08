<template>
    <div v-if="authorised" className="sticky-top">
        <TopNav />
    </div>
    <component :is="viewComponent"/>
</template>

<script>
import router from "./router";
import {auth} from "./service/authcode";

import TopNav from "./components/TopNav.vue";
import NotFound from "./modules/NotFound.vue";
import NotAuthorised from "./modules/NotAuthorised.vue";
import Home from "./modules/Home.vue";
import Tasks from "./modules/Tasks.vue";

const routes =
{
    "/": Home,
    "/tasks": Tasks
}

export default 
{
    name: "App",
    components: { TopNav },
    data()
    {
        return{ 
            auth: auth,
            router: router 
        };
    },
    computed:
    {
        authorised()
        {
            return !!auth.accessToken;
        },
        viewComponent()
        {
            // Not authorised yet - don't render
            if( !this.authorised ) return NotAuthorised;

            // Authorised - pick the route component
            return routes[this.router.state.path] || NotFound;
        }
    },
    created()
    {
        router.startWatching();
    }
};
</script>