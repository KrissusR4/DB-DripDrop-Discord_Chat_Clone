import React, { Component } from 'react';

import './SideDrawer.css';
import ItemList from '../ItemList/ItemList';

import AddFrend from '../AddFrend/AddFrend';
import Add from '../Add/Add';
import Avatar from '../Avatar/Avatar';

class SideDrawer extends Component {
  
  state = {
    results : [],
    addFrend: false,
    addServer: false,
    user: this.props.user,
    servers: [],
    frends: [],
  };

  componentDidMount() {

    const date = new Date();
    const hours = date.getMinutes() / 10 < 1 ? `0${date.getMinutes()}` :  date.getMinutes();
    console.log(`${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${hours}:${date.getSeconds()}.${date.getMilliseconds()}${date.getTimezoneOffset()}`)

    this.props.user.frends.forEach( frend => {

        fetch(`http://localhost:32345/api/User/${frend}`)
        .then(res => res.json())
        .then(frend => this.setState(prevState => { return { frends : [...prevState.frends , 
          { name: frend.username , avatar: frend.nickname , id: frend.userUID}]} }));
    });

    this.props.user.servers.forEach( serverr => {

        fetch(`http://localhost:32345/api/Server/${serverr}`)
        .then(res => res.json())
        //.then(server => console.log("server" , server))
        .then(server => {
          console.log(server);
          this.setState(prevState => { return { servers : [...prevState.servers, 
          {...server , id: server.serverUID} ]} })});
        });
        
  }

  FrendToggleClickHandler = () => {
    this.setState((prevState) => {
      return {addFrend: !prevState.addFrend};
    });
  };
  ServerToggleClickHandler = () => {
    this.setState((prevState) => {
      return {addServer: !prevState.addServer};
    });
  };

  FrendSearchHandler = (event) => {
    this.frendSName =  event.target.value;
  };
  
  serverNameH = (event) => {
    this.newServerName = event.target.value;
  }
  serverAddBtn = () => {
    
    fetch(`http://localhost:32345/api/Server/`, {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            name : this.newServerName,
            password : null,
            privateS : false,
            serverUID: this.props.user.id
          })
        }).then(res => res.json())
        .then( server => this.setState((prevState) => {
          return {servers: [...prevState.servers, server]};
        }));
    
  }
  searchFrendH = (event) => {
    if(event.key === 'Enter')
    {
      fetch(`http://localhost:32345/SearchByName?name=${this.frendSName}`)
      .then( res => res.json())
      .then( sResoults => this.setState( prevState =>{ return  {results : [...sResoults]}} ))
    }
  }
  addFrendH = (frend) => {
    fetch(`http://localhost:32345/PutFriend?myId=${this.props.user.id}&friendId=${frend.userUID}`, {
          method: 'PUT',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({})
        })
        this.setState( prevState =>{ return  {frends : [...prevState.frends , {...frend ,id:frend.userUID, avatar:frend.nickname , name:frend.username}]}} )    
  }
  render() {
    
    let drawerClasses = 'side-drawer';
    if (this.props.show) {
      drawerClasses = 'side-drawer open';
    }
  
  return (
    <div className={drawerClasses}>
        <Avatar link={this.props.user.avatar} size="big"/>
        <p>{this.props.user.name}</p>
          <ItemList clickHandler={this.props.click} drag={this.props.drag} avatar={true} list={this.state.frends}  name="Frends"  expand={this.props.fToggle} show={this.props.toggleF}>
          <AddFrend addH={this.addFrendH} keyH={this.searchFrendH} click={this.FrendToggleClickHandler} show={this.state.addFrend} res={this.state.results} change={this.FrendSearchHandler}></AddFrend>
          </ItemList>
          <ItemList clickHandler={this.props.sClick} avatar={false} list={this.state.servers} name="Servers" expand={this.props.sToggle} show={this.props.toggleS}>
            <Add show={this.state.addServer} click={this.ServerToggleClickHandler} add={this.serverAddBtn} change={this.serverNameH}></Add>
          </ItemList>
 
    </div>
  );
  }
};

export default SideDrawer;
