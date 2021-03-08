<template>
    <div class="module module-page">
        <h1 class="mb-1">Tasks</h1>
        <div v-if="!loaded">Loading...</div>
        <template v-if="loaded">
            <div>
                You have a total of {{ totalCount }} tasks.
                <span v-if="totalCount > 100">Showing only the first 100.</span>
            </div>
            <table>
                <thead>
                    <tr>
                        <th width="150">Due</th>
                        <th width="200">Reference</th>
                        <th width="*">Title</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="task in tasks" :key="task.id">
                        <td>{{ task.due.toLocaleString() }}</td>
                        <td>{{ task.reference }}</td>
                        <td>{{ task.title }}</td>
                    </tr>
                </tbody>
            </table>
        </template>
    </div>
</template>


<script>
import tasksAgent from "../service/tasksAgent";

export default
    {
        name: "Home",
        data()
        {
            return {
                loaded: false,
                totalCount: 0,
                tasks: []
            }
        },
        mounted()
        {
            tasksAgent.getMyTasks().then(data =>
            {
                this.tasks = data.results.map(t =>
                ({
                    reference: t.data.reference,
                    title: t.data.title,
                    due: new Date(t.data["taskDueDate.date.utc.value"]).toLocaleString()
                }));
                this.totalCount = data.totalCount;
                this.loaded = true;
            });
        }
    };
</script>