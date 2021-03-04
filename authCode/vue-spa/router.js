import { reactive } from "vue";

const router = 
{
    watching: false,
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
        if( watching ) return;
        watching = true;
        
        window.addEventListener("popstate", () =>
        {
            this.state.path = window.location.pathname;
        });
    }
};

export default router;
