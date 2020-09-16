import React, { Component } from 'react';
import './ChatBox.css';
import ChatMsg from './ChatMsg';
import ChatHeader from '../ChatHeader';



class chatBox extends Component{

    componentDidMount() {

        this.message = '';
        // setInterval( () => {

        //     if (this.props.chatUser !== undefined && this.props.chat[0] !== undefined) {
        //         let lastFrendMsg = null;
        //         let i = 0;
        //         while(lastFrendMsg === null){
    
        //             if (this.props.chat[i].sender === this.props.chatUser.id) {
        //                 lastFrendMsg = this.props.chat[i];
        //             }
        //             i++;
        //         }
                
        //         this.props.updateChatMsgs(lastFrendMsg.id, this.props.user.id , this.props.chatUser.id)
        //         console.log(lastFrendMsg);
        //         console.log()
        //     }
        // },30000)
    }

    inputHandler = event => {
        this.message = event.target.value;
        console.log(this.message);
    }
    sendHandler = () => {
        console.log(this.props.chat);
        if(this.message !== ''){

        
        fetch(`http://localhost:32345/api/Message/`, {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            text: this.message,
            fromWho : this.props.user.id,
            picture : null,
            chatUID : this.props.chatData.chatUID
          })
        })
    
        this.props.chatChange( { text: this.message,
            fromWho : this.props.user.id,
            picture : null,
            chatUID : this.props.chatData.chatUID});
        }
        //this.props.updateChat( parseInt(`${Today.getHours()}${Today.getMinutes()}`),this.props.chat[this.props.chat.length-1].id,this.props.user.id, this.props.chatUser.id)
    }
    render(){ 

        if(this.props.sent){
            //console.log(this.props.chat[0].id, "chat id");
            //this.props.updateChat(this.props.chat[this.props.chat.length-1].id,this.props.user.id, this.props.chatUser.id);
        }
        
        let message = this.props.chat.map( poruka =>{
            if(poruka.fromWho !== this.props.user.id){
                return (<ChatMsg  person="chat friend" message={poruka.text} url={this.props.chatUser.userPic} key={poruka.messageUID}/>);
            }else{
                return (<ChatMsg  person="chat self" message={poruka.text} url={this.props.user.userPic} key={poruka.messageUID}/>);
            }
        });
        return(
    <div className={`chatbox ${this.props.hide} ${this.props.close}`}>
        {/* <ChatHeader userId={this.props.chatUser.id} closed={this.props.close}  pic={this.props.chatUser.userPic} name={this.props.chatUser.name}/> */}
        <div className={`chatlogs ${this.props.hide}`}>
        {message}
        <p className="load-more"  onClick={this.props.LoadMoreHandler}>Load More Messages </p>
        </div>
        <div className={`chat-form ${this.props.hide}`}>
                <textarea onChange={(event) => this.inputHandler(event)} value={this.props.message}></textarea>
                <button onClick={this.sendHandler}>Send</button>
            </div>
    </div>
        );
    }
}
  
export default chatBox;
