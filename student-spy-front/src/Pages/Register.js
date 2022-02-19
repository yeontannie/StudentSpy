import React from "react";
import { Form, Input, Button } from "antd";
import { InputNumber } from 'antd';
import authService from "../Services/authService";

function Register() {
    const [username, setName] = React.useState('');
    const [lastname, setLastName] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [age, setAge] = React.useState(parseInt(''));
    const [password, setPassword] = React.useState('');
    const [confirmation, setConfirm] = React.useState('');

    const takeUserNameValue = (e) => {setName(e.target.value)}
    const takeLastNameValue = (e) => {setLastName( e.target.value)}
    const takeEmailValue = (e) => {setEmail(e.target.value)}
    const takeAgeValue = (e) => {setAge(parseInt(e.target.value))}
    const takePasswordValue = (e) => {setPassword(e.target.value)}
    const takeConfirmValue = (e) => {setConfirm(e.target.value)}

    var userData = {
        name: username,
        lastName: lastname,
        email: email,
        password: password,
        age: age,
        photoPath: ""
    }

    function submitRegister() {
        if(password === confirmation)
        {        
            authService.registerUser(userData)
            .then(function (response) {
                console.log(response)
                handleSubmit()
            })
            .catch(function (error) {
                console.log(error)
        })}
        else{
            alert("Passwords do not match")
        }
    }

    async function handleSubmit() {     
        try {
            window.location.href = '/confirm-email';         
        } catch (e) {
          console.log(e.message);
        }
    }

    return (
        <div className="registerContainer">
            <Form
            name="basic"
            labelCol={{
                span: 8,
            }}
            wrapperCol={{
                span: 16,
            }}            
            autoComplete="off"
            >
            <Form.Item
                label="Name"
                name="username"
                id="username"
                onChange={takeUserNameValue} 
                rules={[
                {
                    required: true,
                    message: 'Please input your Name!',
                },
                ]}
            >
                <Input />
            </Form.Item>

            <Form.Item
                label="Last Name"
                name="lastname" 
                id="lastname"
                onChange={takeLastNameValue}                
                rules={[
                {
                    required: true,
                    message: 'Please input your Last Name!',
                },
                ]}
            >
                <Input />
            </Form.Item>

            <Form.Item
                label="Email"
                name="email" 
                id="email"
                onChange={takeEmailValue}                
                rules={[
                {
                    required: true,
                    message: 'Please input your email!',
                },
                ]}
            >
                <Input />
            </Form.Item>

            <Form.Item
                label="Age"
                name="age"
                id="age" 
                onChange={takeAgeValue}                                         
                rules={[
                {
                    required: true,
                    message: 'Please input your age!',
                },
                ]}
            >
                <InputNumber
            min={1}
            max={40}
            style={{ margin: '0 16px' }}
          />
            </Form.Item>

            <Form.Item
                label="Password"
                name="password"
                id="password"
                onChange={takePasswordValue}               
                rules={[
                {
                    required: true,
                    message: 'Please input your password!',
                },
                ]}
            >
                <Input.Password />
            </Form.Item>

            <Form.Item
                label="Confirmation"
                name="confirmation"
                id="confirmation"
                onChange={takeConfirmValue}               
                rules={[
                {
                    required: true,
                    message: 'Please repeat your password!',
                },
                ]}
            >
                <Input.Password />
            </Form.Item>
            
            <Form.Item
                wrapperCol={{
                offset: 8,
                span: 16,
                }}
            >
                <Button type="primary" htmlType="submit" onClick={submitRegister}>
                Register
                </Button>
            </Form.Item>
            </Form>
        </div>       
    );    
}

export default Register;