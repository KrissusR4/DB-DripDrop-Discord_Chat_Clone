import React from 'react';
import './Input.css';

const input = (props)  => (
    <div className="input-style">
        <p>{props.label}</p>
        <input type={props.type} placeholder={props.placeHoleder} onInputCapture={ (event) => props.handler(event) } />
        <div className="line" />
    </div>
);


export default input;

