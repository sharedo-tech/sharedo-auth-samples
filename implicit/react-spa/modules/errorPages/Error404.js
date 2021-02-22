import React from "react";

class Error404 extends React.Component
{
    render()
    {
        return (
            <div className="module module-page">
                <h1 className="mb-1">404 Not Found</h1>
                <div>We can't find that URL</div>
            </div>
        );
    }
}

export default Error404;