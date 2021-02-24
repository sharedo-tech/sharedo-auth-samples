import React from "react";
import { connect } from "react-redux";

import tasksAgent from "../../model/agents/tasksAgent";

class PageTasks extends React.Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            loaded: false,
            totalCount: 0,
            tasks: []
        };
    }

    componentDidMount()
    {
        this.load();
    }
    
    componentDidUpdate()
    {
        this.load();
    }

    load()
    {
        console.log("A");
        if (!this.state.loaded && this.props.userId)
        {
            tasksAgent.getMyTasks(this.props.userId).then(data =>
            {
                this.setState(
                    {
                        loaded: true,
                        totalCount: data.totalCount,
                        tasks: data.results.map(r =>
                        ({
                            reference: r.data.reference,
                            title: r.data.title,
                            due: new Date(r.data["taskDueDate.date"])
                        }))
                    });
            });
        }
    }

    render()
    {
        if (!this.state.loaded) return null;

        return (
            <div className="module module-page">
                <h1 className="mb-1">Tasks</h1>
                <div>
                    You have a total of {this.state.totalCount} tasks.
                    {this.state.totalCount > 100 && <span>Showing only the first 100.</span>}
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
                        {this.state.tasks.map((t, i) =>
                            <tr key={i}>
                                <td>{t.due.toLocaleString()}</td>
                                <td>{t.reference}</td>
                                <td>{t.title}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}

const mapStateToProps = (state, ownProps) => ({
    userId: state.auth.userId
});

const mapActionsToProps = ({});

export default connect(mapStateToProps, mapActionsToProps)(PageTasks);
