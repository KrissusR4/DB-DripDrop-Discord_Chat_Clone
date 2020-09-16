import './ItemList.css';
import React from 'react';

import Avatar from '../Avatar/Avatar';
const itemList = props => { 
    
    let listClass = 'list';
    if (props.show) {
        listClass = 'list open';
    }
    console.log(props.show);
    let list;
    if(!props.avatar)
    {
      list = props.list.map(element => <p  onClick={() => props.clickHandler(element)} className="item" key={element.name}>{element.name}</p>);
    }else {
    list = props.list.map(element => { 
    return (
    <div  className="item-con" key={element.name}>
      <Avatar link={element.avatar} size="small"></Avatar>
    <p  onMouseDown={() => props.drag(element)} onClick={ () => props.clickHandler(element)}  >{element.name}</p>
    </div> 
    )});
    }
    return(
      <div className="item-list">
        <p onClick={props.expand}  onContextMenu={props.drag}  >{props.name}</p>
        <div className={listClass}>
            {list} 
            {props.children}
        </div>
      </div>  
    )};


export default itemList