import React, { Component } from 'react';
import { Route } from "react-router";

import Input from '../../components/Input/Input';
import './Login.css';
// import {Router, Route  } from 'react-router';


class Login extends Component {

    render() {


        return (
            <div className="login-page">
                <img src="https://wallpapermemory.com/uploads/654/purple-wallpaper-hd-1920x1080-405334.jpg" />
                <div className="login-card">
                    <img src={require("../../assets/test4.png")}/>
                    <Input placeHoleder={"example@gmail.com"} label="User Name" type="text" handler= {this.props.nameHandler}/>
                    <Input placeHoleder={""} label="Password" type="Password" handler= { this.props.passHandler}/>
                    <Route render={({ history}) => (
                        <button
                            type='button'
                            onClick={() => { 
                                this.props.loginHandler()
                                .then( success => { 
                                    if(success === true) 
                                    history.push('/Home'); 
                                });
                            }}
                            >
                            Login
                        </button>
                    )} />
                    <Route render={({ history}) => (
                        <button
                            type='button'
                            onClick={() => { history.push('/register') }}
                            >
                            Register
                        </button>
                    )} />
                </div>
            </div>
        );
    }
}

export default Login;
