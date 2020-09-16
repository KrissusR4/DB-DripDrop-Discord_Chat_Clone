import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route } from "react-router-dom";
// import {Router, Route  } from 'react-router';

import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';

class App extends Component {

  // constructor(){
  
    user = {
      id : "miske",
      userPic : "./logo.svg",
      frends : [],
      servers: []
    };
  
  PassHandler = (event) => { this.pass = event.target.value};
  NameHandler = (event) => { this.name = event.target.value};
  LoginHandler = () => { 

   return new Promise(resolve => {
    fetch(`http://localhost:32345/GetByLogin?name=${this.name}&pass=${this.pass}`)
    .then( res => res.json())
    .then ( user => {
      console.log(user);
      if(user.username !== null){
        this.user = { id: user.userUID, name: user.username , servers: user.serverUIDsList , frends: user.friendUIDsList , avatar: user.nickname, nick: user.nickname};
        console.log('Done')
        resolve(true);
      }else{ resolve(false)}
    })
   }); 
  };
  render() {

    return (
      <div style={{height: '100%'}} className="App">
        <Router >
        <Route exact path={"/"} render={ (props) => <Login passHandler={this.PassHandler} nameHandler={this.NameHandler} loginHandler={this.LoginHandler} />}/>
            <Route path={"/Home"} render={ (props) => <Home user={this.user} />} />
            <Route  path={"/register"} component={Register}/>
            {/* <Route path={"register"} component={}/> */}
        </Router>
      </div>
    );
  }
}

export default App;
