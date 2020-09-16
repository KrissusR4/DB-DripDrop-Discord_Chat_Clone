import './Add.css';
import React from 'react';

const Add = props => { 

    let classes = 'add-con';
    if (props.show) {
        classes = 'add-con open';
    }

    return (<div className="add">
        <p onClick={props.click}> + </p>
        <div className={classes}>
            <input onChange={props.change} type="text"></input>
            <button onClick={props.add}>Add</button>
        </div>
    </div>);

}


export default Add;