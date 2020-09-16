import React from 'react';
import './ChatHeader.css';
import Avatar from '../Avatar/Avatar';


const chatHeader = (props) => (
    <div className={`chat-header `} >
        <Avatar size="small" link={props.pic} />
        <p> {props.name}</p>        
        <div className='spacer'/>
        <i  className="fas fa-times"></i>
 
    </div>
);

export default chatHeader;