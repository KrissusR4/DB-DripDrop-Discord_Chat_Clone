import React from 'react';
import './ChatMsg.css'
import Avatar from '../../Avatar/Avatar';


const chatMsg = (props) => (
    <div className={props.person}>
                <Avatar size="small" link={props.url} />
                <p className="chat-message"> {props.message}</p>
    </div>
);

export default chatMsg;