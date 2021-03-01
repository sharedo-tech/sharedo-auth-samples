<template>
    <div className="sticky-top">
        <TopNav />
    </div>
    <Router />
</template>



<script>
import TopNav from "./components/TopNav.vue";
import Router from "./Router";

export default 
{
    name: "App",
    components: { TopNav, Router }
};
</script>