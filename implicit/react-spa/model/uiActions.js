export const UI_DRAWER_OPEN = "UI_DRAWER_OPEN";
export const UI_DRAWER_CLOSE = "UI_DRAWER_CLOSE";

export function openDrawer()
{
    return { 
        type: UI_DRAWER_OPEN
    };
}

export function closeDrawer()
{
    return {
        type: UI_DRAWER_CLOSE
    };
}