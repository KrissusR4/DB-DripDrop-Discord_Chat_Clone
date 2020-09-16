import React, { Component } from 'react';
// import './Home.css'

import Toolbar from '../../components/Toolbar/Toolbar';
import SideDrawer from '../../components/SideDrawer/SideDrawer';
import Backdrop from '../../components/Backdrop/Backdrop';
import ChatBox from '../../components/Chat/ChatBox/ChatBox';
import ItemList from '../../components/ItemList/ItemList';
import Add from '../../components/Add/Add';

class Home extends Component {
  state = {
    sideDrawerOpen: false,
    frendListOpen: true,
    serversListOpen: false,
    addChatsListOpen: false,
    frendList : [   
    // {name: "miske"},
    // {name: "ivan"},
    // {name: "JOca"}
  ],
  serversList: [
    // {name : "fagets"},
    // {name : "jeb se"}
  ],
  chatU : {
    id : "ziva",
    userPic : "./logo.svg"
  },
  user : {
    name : "KrisKum",
    userPic : "https://scontent.fbeg1-1.fna.fbcdn.net/v/t1.0-9/p720x720/31948539_2124710084223154_6396547342857666560_o.jpg?_nc_cat=106&_nc_ohc=I_DuxEMVp0MAX9QmUL6&_nc_ht=scontent.fbeg1-1.fna&oh=0de96b254eb68c2f1a73312b14f05038&oe=5EAF12F0",
  },
  chat : [
    {from : "miske", message: "jeb se", id: 1},
    {from : "ziva", message: "i ti", id: 2},
  ],
  server: { chatUIDsList: [] , serverUID: 0},
  chats: [],
  result: [],
  messages: [],
 };

  
  frendsToggleOpen = () => {
    this.setState((prevState) => {
      return {frendListOpen: !prevState.frendListOpen};
    });
  };
  chatsToggleOpen = () => {
    this.setState((prevState) => {
      return {addChatsListOpen: !prevState.addChatsListOpen};
    });
  };

  serversToggleOpen = () => {
    this.setState((prevState) => {
      return {serversListOpen: !prevState.serversListOpen};
    });
  };

  drawerToggleClickHandler = () => {
    this.setState((prevState) => {
      return {sideDrawerOpen: !prevState.sideDrawerOpen};
    });
  };

  backdropClickHandler = () => {
    this.setState({sideDrawerOpen: false});
  };

  itemClick = (user) => {
    fetch(`http://localhost:32345/api/Chat/${user.id}`)
    .then(res => res.json())
    .then(newChat => this.setState(prevState => { return { chat : newChat} }));
  };

  serverClick = (serverr) => {
     this.setState(prevState => { return { server : serverr} });
     this.setState(prevState => { return { chats : []} });
     serverr.chatUIDsList.forEach( chat => {

      fetch(`http://localhost:32345/api/Chat/${chat}`)
      .then(res => res.json())
      .then(chatt => this.setState(prevState => { return { chats : [...prevState.chats , 
        { ...chatt, name: chatt.name , id: chatt.chatUID}]} }));
  });
  };
  
  chatClick = (chat) => {
    this.setState(prevState => { return   { chat : chat}})
    const date = new Date();
    const hours = date.getMinutes() / 10 < 1 ? `0${date.getMinutes()}` :  date.getMinutes();
    const chatDate = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${hours}:${date.getSeconds()}.${date.getMilliseconds()}`;

    fetch(`http://localhost:32345/GetMessages?time=${chatDate}&chatId=${chat.id}`)
    .then(res => res.json())
    .then(newMessages => this.setState(prevState => { return  newMessages[0] !== null ? { messages : [...newMessages]} : null}));
  };

  AddChatHandler = () => {
    
    fetch(`http://localhost:32345/api/Chat/`, {
            method: 'POST',
            headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              name : this.chatName,
              serverUID : this.state.server.serverUID,
            })
          }).then( res => res.json())
          .then(chat => this.setState(prevState => { return { chats : [...prevState.chats, chat]} }))
  }
  chatNameHandler = (event) => {
    this.chatName = event.target.value;
  }
  messageAdd = (message) => {
    this.setState(prevState => { return   { messages : [message ,...prevState.messages ]}})
  }
  addFrendToServ = (frend) => {
    fetch(`http://localhost:32345/PutServer?myId=${frend.id}&serverId=${this.state.server.serverUID}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({})
    })
  }
  render() {
    let backdrop;

    if (this.state.sideDrawerOpen) {
      backdrop = <Backdrop click={this.backdropClickHandler} />
    }
    return (
      <div style={{height: '100%'}}>
        <Toolbar  drawerClickHandler={this.drawerToggleClickHandler} />
        <SideDrawer drag={this.addFrendToServ} click={this.itemClick} sClick={this.serverClick} fToggle={this.frendsToggleOpen} sToggle={this.serversToggleOpen} toggleF={this.state.frendListOpen}
        toggleS={this.state.serversListOpen} frends={this.state.frendList} servers={this.state.serversList}
        show={this.state.sideDrawerOpen} user={this.props.user} />
        {backdrop}
        <main style={{marginTop: '56px' }}>
        <div style={{flexGrow : "1" }}>
          <div style={ this.state.server.chatUIDsList.length === 0 ? {display: 'none'} : {minWidth: '235px', marginTop: "35px"} }>
            <ItemList  clickHandler={this.chatClick} avatar={false} list={this.state.chats}  name="Chats"  expand={() => 0} show={true}>
              <Add add={this.AddChatHandler} change={this.chatNameHandler} show={this.state.addChatsListOpen} click={this.chatsToggleOpen}/>
            </ItemList>
          </div>
        </div>
        <ChatBox chatChange={this.messageAdd}chatData={this.state.chat} user={this.props.user} chatUser={this.state.chatU} chat={this.state.messages} ></ChatBox>
        <div style={{flexGrow : "1"}}></div>
        </main>
        
      </div>
    );
  }
}

export default Home;
