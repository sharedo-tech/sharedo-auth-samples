import settings from "../../settings";
import {post} from "./fetchWrapper";

function getMyTasks(userId)
{
    return post(`${settings.api}/api/v1/public/workItem/findByQuery`,
    {
        search:
        {
            page: { rowsPerPage: 100, page: 1 },
            types: 
            { 
                includeTypes: ["task"],
                includeTypesDerivedFrom: ["task"]
            },
            phase:
            {
                includeRemoved: false
            },
            ownership:
            {
                myScope:
                {
                    ownerIds:[userId],
                    primary: true,
                    secondary: false
                }
            }
        },
        enrich:
        [
            { path: "reference" },
            { path: "title" },
            { path: "taskDueDate.date" }
        ]
    });
}

export default 
{
    getMyTasks
};
