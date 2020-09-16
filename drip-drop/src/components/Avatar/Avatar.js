import React from 'react';
import './Avatar.css'


const avatar = props => (
    <div className={props.size}>
        <img src={props.link}/>
    </div>
);

export default avatar;