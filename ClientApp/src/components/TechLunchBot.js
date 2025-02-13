import React, { Component } from 'react';
import './home.css'

export class TechLunchBot extends Component {
    static displayName = TechLunchBot.name;

    constructor(props) {
        super(props);
        this.state = {
            messages: [],
            newMessage: ''
        };
    }

    render() {
        return (
            <div className="bot-container">
                <div className="centered-statement">
                    Hello In our store
                </div>
            </div>
        );
    }
}

export default TechLunchBot; 