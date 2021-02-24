<template>
    <a v-bind:href="to" @click="clicked">
        <slot></slot>
    </a>
</template>

<script>
export default {
    props: ["to"],
    emits: ["click"],
    methods:
    {
        clicked(e)
        {
            e.preventDefault();
            history.pushState(null, null, this.to);
            this.$emit("click");
        }
    }
};
</script>
