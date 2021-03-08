import settings from "../settings";
import {post} from "./fetchWrapper";
import {profile} from "./profile";

function getMyTasks()
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
                    ownerIds:[profile.userId],
                    primary: true,
                    secondary: false
                }
            }
        },
        enrich:
        [
            { path: "reference" },
            { path: "title" },
            { path: "taskDueDate.date.utc.value" } 
        ]
    });
}

export default 
{
    getMyTasks
};
