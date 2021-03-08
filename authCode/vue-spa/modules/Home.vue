<template>
    <div class="module module-page">
        <h1 class="mb-1">Home</h1>
        <div>Welcome to my-sharedo {{profile.name}}</div>
    </div>
</template>


<script>
import {profile} from "../service/profile";

export default 
{
    name: "Home",
    data()
    {
        return {
            profile: profile
        }
    }
};
</script>