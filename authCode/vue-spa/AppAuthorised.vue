<template>
    <div className="sticky-top">
        <TopNav />
    </div>
    <component :is="viewComponent"/>
</template>



<script>
import router from "./router";
import TopNav from "./components/TopNav.vue";
import NotFound from "./modules/NotFound.vue";
import Home from "./modules/Home.vue";
import Tasks from "./modules/Tasks.vue";

const routes =
{
    "/": Home,
    "/tasks": Tasks
}

export default 
{
    name: "AppAuthorised",
    components: { TopNav },
    data()
    {
        return{ router: router };
    },
    computed:
    {
        viewComponent()
        {
            return routes[this.router.state.path] || NotFound;
        }
    },
    created()
    {
        router.startWatching();
    }
};
</script>