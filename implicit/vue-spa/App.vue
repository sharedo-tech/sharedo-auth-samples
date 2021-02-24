<template>
    <div className="sticky-top">
        <TopNav />
    </div>
<!--                <Switch>
                    <Route path="/" exact component={PageHome} />
                    <Route path="/tasks" exact component={PageTasks} />
                    <Route component={Error404} />
                </Switch>

    <div>Hello 1</div>
    <div>Hello 3</div>-->
</template>



<script>
import TopNav from "./components/TopNav.vue";

export default 
{
    name: "App",
    components: { TopNav }
};
</script>