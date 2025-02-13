import React, { Component } from 'react';
import './Bot.css'

export class Bot extends Component {
    static displayName = Bot.name;

    constructor(props) {
        super(props);
        this.state = {
            messages: [],
            newMessage: ''
        };
    }

    handleSubmit = async (e) => {
        e.preventDefault();
        if (this.state.newMessage.trim()) {
            this.setState(prevState => ({
                messages: [...prevState.messages, { text: this.state.newMessage, sender: 'user', id: Date.now() }],
                newMessage: ''
            }));

            const msgs = [...this.state.messages, { text: this.state.newMessage, sender: 'user', id: Date.now() }];

            try {
                const response = await fetch(`chat`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ messages: msgs })
                });
                const data = await response.json();

                this.setState(prevState => ({
                    messages: [...prevState.messages, { text: data.text, sender: 'assistant', id: Date.now() }]
                }));
            } catch (error) {
                console.error('Error fetching response:', error);
            }
        }
    };

    cleraChat = async (e) => {
        this.setState({
            messages: [],
            newMessage: ''
        });
    };

    renderMessages() {
        return (
            <div className="messages">
                {this.state.messages.map((message) => (
                    <div
                        key={message.id}
                        className={`message ${message.sender}`}
                    >
                        {message.text}
                    </div>
                ))}
            </div>
        );
    }

    render() {
        return (
            <div className="chat-container">
                {this.renderMessages()}

                <form onSubmit={this.handleSubmit} className="message-form">
                    <input
                        type="text"
                        value={this.state.newMessage}
                        onChange={(e) => this.setState({ newMessage: e.target.value })}
                        placeholder="Type a message..."
                        className="message-input"
                    />
                    <button type="submit" className="send-button">
                        Send
                    </button>
                    <button type="submit" className="clear-button" onClick={this.cleraChat}>
                        Clear
                    </button>

                </form>
            </div>
        );
    }
}

export default Bot; 