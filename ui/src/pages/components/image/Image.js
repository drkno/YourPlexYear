import React, { Component } from 'react';

class Image extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loaded: false
        }
    }

    handleImageLoaded() {
        this.setState({ loaded: true });
    }

    render() {
        const style = Object.assign({
            opacity: this.state.loaded ? void(0) : 0
        }, this.props.style || {});
        return (
            <img className={this.props.className || ''} src={this.props.src} style={style} onLoad={this.handleImageLoaded.bind(this)} />
        );
    }
}

export default Image;
