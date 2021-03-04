<template>
    <li v-if="to" class="link">
        <Link v-bind:to="to" @click="$emit('beforeNav')">
            <span v-if="icon" class="fa fa-fw" :class="icon"></span>
            {{title}}
        </Link>
    </li>
    <li v-if="!to" class="title">{{title}}</li>
</template>

<script>
import Link from "./Link.vue";

export default {
    components: { Link },
    props: ["to", "icon", "title"],
    emits: ["beforeNav"]
};
</script>
