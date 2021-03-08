<script>
import {h} from "vue";
import {auth} from "./service/authcode";

import AppAuthorised from "./AppAuthorised.vue";

const NotAuthorised = { template: "<p>You are not authorised</p>"};


export default 
{
    name: "App",
    components: { },
    data()
    {
        return{ auth: auth };
    },
    computed:
    {
        viewComponent()
        {
            return auth.accessToken ? AppAuthorised : NotAuthorised;             
        }
    },
    render()
    {
        return h(this.viewComponent);
    }
};
</script>

