import React from 'react';

//shows total time of being in the page
//the button resets the time

class Timer extends React.Component {
    constructor(props) {
        super(props);
        let storedTime = localStorage.getItem("startTime");
        this.state = { startTime: storedTime ? Number(storedTime) : Date.now() };
    }

    componentDidMount() {
        if (!localStorage.getItem("startTime")) {
            localStorage.setItem("startTime", this.state.startTime);
        }
        this.interval = setInterval(() => {
            let currentTime = Date.now();
            let timeSpent = (currentTime - this.state.startTime) / 1000;
            this.setState({ timeSpent });
        }, 1000);
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    handleReset = () => {
        localStorage.clear();
        clearInterval(this.interval);
    };

    render() {
        let seconds = ('0' + Math.floor(this.state.timeSpent % 60)).slice(-2);
        let minutes = ('0' + Math.floor((this.state.timeSpent / 60) % 60)).slice(-2);
        let hours = ('0' + Math.floor(this.state.timeSpent / 3600)).slice(-2);

        return <div>Time spent on page: {hours}:{minutes}:{seconds} <input type="button" className="btn-primary" value="Stop Counting" onClick={this.handleReset} /></div>;
    }
}
export default Timer;




