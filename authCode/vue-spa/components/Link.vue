<template>
    <a v-bind:href="to" @click="clicked">
        <slot></slot>
    </a>
</template>

<script>
import router from "../router";

export default {
    props: ["to"],
    emits: ["click"],
    methods:
    {
        clicked(e)
        {
            e.preventDefault();
            router.setPath(this.to);
            this.$emit("click");
        }
    }
};
</script>
