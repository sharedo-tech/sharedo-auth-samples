<script>
import {h} from "vue";
import {auth} from "./service/authcode";

const NotAuthorised = { template: "<p>You are not authorised</p>"};
const Authorised = { template: "<p>SHOW APP</p>"};

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
            return auth.accessToken ? Authorised : NotAuthorised;             
        }
    },
    render()
    {
        return h(this.viewComponent);
    }
};
</script>