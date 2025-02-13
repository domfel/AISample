import React, { Component } from 'react';
import './Bot.css'

export class BotSecure extends Component {
    static displayName = BotSecure.name;

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
                const response = await fetch(`chatsecure`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ messages: msgs })
                });
                const data = await response.json();

                this.setState(prevState => ({
                    messages: [...prevState.messages, { text: data.text, sender: 'assistant', id: Date.now(), hasOrder: data.hasOrder, orders: data.orders }]
                }));
            } catch (error) {
                console.error('Error fetching response:', error);
                return { text: error, hasOrder: false, orders: [] };
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
                {this.state.messages.map((message) => {
                    if (!message.hasOrder) {
                        return (
                            <div
                                key={message.id}
                                className={`message ${message.sender}`}
                            >
                                {message.text}
                            </div>
                        );
                    } else {
                        return (
                            <div
                                key={message.id}
                                className={`message ${message.sender}`}
                            >
                                {message.text}: <br />
                                {message.orders.map((order) => (
                                    <div key={order.id}>
                                        <ul>
                                            <li>
                                                ProductId: {order.productId}, CustomerName: {order.customerName}, Quantity: {order.quantity}
                                            </li>
                                        </ul>
                                        <button type="submit" className="checkout-button">
                                            Continue with the order
                                        </button>
                                    </div>
                                ))}
                            </div>
                        );
                    }
                })}
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

export default BotSecure; 