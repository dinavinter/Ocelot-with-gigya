import React, { Component } from 'react';

export class Counter extends Component {
  static displayName = Counter.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
   }

 async incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });

  
  }

  render() {
    return (
      <div>
        <h1>Posts</h1>
 
        <p aria-live="polite">Current post id: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Fetch</button>

        <p aria-live="polite">Current post id: <strong>{this.state.currentCount}</strong></p>
          
          

      </div>
    );
  }
}
