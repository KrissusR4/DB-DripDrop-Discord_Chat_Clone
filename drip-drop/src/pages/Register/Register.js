import React,{ Component } from "react";
import { Route } from "react-router";
import './Register.css';

import Input from '../../components/Input/Input';


class Register extends Component {

    state = {
        name: '',
        gmail: '',
        pass: '',
        picture: ''
    }
    nameHandler = (event ) =>{
        this.setState({ name : event.target.value} );
    }
    gmailHandler = (event ) =>{
        this.setState({ gmail : event.target.value} );
    }
    passHandler = (event ) =>{
        this.setState({ pass : event.target.value} );
    }
    rePassHandler = (event ) =>{
        this.setState({ picture : event.target.value} );
    }
    Register = () => {
        fetch(`http://localhost:32345/api/User/`, {
            method: 'POST',
            headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              username : this.state.name,
              password : this.state.pass,
              nickname : this.state.picture,
            })
          })
    }
   
   
    render() {

        return (
                <div className="register-page">
                <img src="https://wallpapermemory.com/uploads/654/purple-wallpaper-hd-1920x1080-405334.jpg" />
                <div className="register-card">
                    <Input placeHoleder={"Ljubisa"} label="Name" type="text" handler={this.nameHandler}/>
                    <Input placeHoleder={"example@gmail.com"} label="Gmail" type="text" handler={this.gmailHandler}/>
                    <Input placeHoleder={""} label="Password" type="password" handler={this.passHandler} />
                    <Input placeHoleder={""} label="Picture" type="password" handler={this.rePassHandler}/>
                    <Route render={({ history}) => (
                        <button
                            type='button'
                            onClick={() => { 
                                this.Register();
                                history.push('/'); }}
                            >
                            Register
                        </button>
                    )} />
                    <Route render={({ history}) => (
                        <button
                            type='button'
                            onClick={() => { history.push('/') }}
                            >
                            Back
                        </button>
                    )} />
                </div>
            </div>
        );
    }
}

   export default Register;
