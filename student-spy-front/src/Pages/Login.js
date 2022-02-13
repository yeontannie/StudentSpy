import React from "react";
import { Form, Input, Button } from "antd";
import authService from "../Services/authService";

function Login() {
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [role, setRole] = React.useState('');

    const takeEmailValue = (e) => {setEmail(e.target.value)}
    const takePasswordValue = (e) => {setPassword(e.target.value)}

    var userData = {
        email: email,
        password: password
    }

    function userLogin() {
        authService.loginUser(userData)
        .then(function (response) {
            console.log(response)                      
            setRole(response.data.role)
            localStorage.setItem('userData', JSON.stringify(response.data))  
            localStorage.setItem('userRole', JSON.stringify(response.data.role[0]))
            localStorage.setItem('accessToken', JSON.stringify(response.data.token))
            handleSubmit(response.data.role[0])
        })
        .catch(function (error) {
            console.log(error)
        })
    };

    async function handleSubmit(event) {     
        try {
            window.location.href = '/';            
        } catch (e) {
          console.log(e.message);
        }
      }


    return (
        <div className="loginContainer">
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
            wrapperCol={{
            offset: 8,
            span: 16,
            }}
        >
            <Button type="primary" htmlType="submit" onClick={userLogin}> 
            Login
            </Button>
        </Form.Item>
        </Form>
        </div>
    );
}

export default Login;