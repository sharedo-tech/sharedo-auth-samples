import { reactive } from "vue";

const router = 
{
    state: reactive({
        path: window.location.pathname
    }),

    setPath(newPath, replace)
    {
        if( replace )
        {
            history.replaceState(null, null, newPath);
        }
        else
        {
            history.pushState(null, null, newPath);
        }
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

window.router = router;

export default router;
