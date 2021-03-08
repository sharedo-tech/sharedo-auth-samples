<template>
    <nav class="navbar fixed">
        <div class="navbar-brand">
            <span class="text-muted mr-sm mq-tablet-or-lower" @click="openDrawer">
                <span class="fa fa-bars"></span>
            </span>
            <h1>
                <Link to="/">my<span class="text-primary">Sharedo</span></Link>
            </h1>
        </div>
        <div className="navbar-group right">
            <span>{{ profile.name || "Who are you?" }}</span>
            <span v-if="profile.name">&nbsp;|&nbsp; <a href="#" @click="signOut">Sign out</a></span>

        </div>
    </nav>

    <nav v-bind:class="drawerClasses">
        <div className="drawer-overlay" @click="closeDrawer"></div>
        <div className="drawer-content">
            <div className="drawer-nav">
                <ul>
                    <TopNavItem title="Main Menu" />
                    <TopNavItem icon="fa-home" title="Home" to="/" @beforeNav="closeDrawer" />
                    <TopNavItem icon="fa-list" title="Tasks" to="/tasks" @beforeNav="closeDrawer" />
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
import Link from "./Link.vue";
import TopNavItem from "./TopNavItem.vue";
import {logout} from "../service/authcode";
import {profile} from "../service/profile";

export default {
    components: { Link, TopNavItem },
    data()
    {
        return {
            open: false,
            profile: profile
        };
    },
    computed:
    {
        drawerClasses()
        {
            if (this.open) return "drawer open";
            return "drawer";
        },
    },
    methods:
    {
        openDrawer()
        {
            this.open = true;
        },
        closeDrawer()
        {
            this.open = false;
        },
        signOut(e)
        {
            e.preventDefault();
            logout();
        }
    }
};
</script>
