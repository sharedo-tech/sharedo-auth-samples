import { h, reactive } from "vue";

import NotFound from "./modules/NotFound.vue";
import Home from "./modules/Home.vue";
import Tasks from "./modules/Tasks.vue";

export const router =
{
    state: reactive({
        path: window.location.pathname
    }),

    setPath(newPath)
    {
        history.pushState(null, null, newPath);
        this.state.path = newPath;
    },

    startWatching()
    {
        window.addEventListener("popstate", () =>
        {
            this.state.path = window.location.pathname;
        });
    }
};

const routes =
{
    "/": Home,
    "/tasks": Tasks
};

export default
    {
        name: "Router",
        data()
        {
            return { router: router };
        },
        computed:
        {
            view()
            {
                return routes[this.router.state.path] || NotFound;
            }
        },
        created()
        {
            router.startWatching();
        },
        render()
        {
            return h(this.view);
        }
    };
