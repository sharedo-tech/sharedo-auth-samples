<template>
    <div class="module module-page">
        <h1 class="mb-1">Home</h1>
        <div>Welcome to my-sharedo {{auth.name}}</div>
    </div>
</template>


<script>
import implicit from "../service/implicit";

export default 
{
    name: "Home",
    data()
    {
        return {
            auth: implicit.auth
        }
    }
};
</script>