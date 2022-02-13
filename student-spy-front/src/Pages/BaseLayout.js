import React from "react";
import { Link } from "react-router-dom";
import { Layout, Menu } from 'antd';

const { Header, Content, Footer } = Layout;
const Base = () => {
    return(   
        <div className="navSystem">
            <Layout>
                <Header style={{ position: 'fixed', zIndex: 1, width: '100%' }}>
                    <div className="logo" />
                    <Menu theme="dark" mode="horizontal">
                        { localStorage.getItem('userData') ?
                        <>
                        <Menu.Item key="1"><Link to="/" className="homePage">Home</Link></Menu.Item>             
                        <Menu.Item key="2"><Link to="/courses" className="homePage">Courses</Link></Menu.Item>
                        <Menu.Item key="5"><Link to="/logout" className="logoutPage">LogOut</Link></Menu.Item>  
                        </> : <>
                        <Menu.Item key="3"><Link to="/login" className="loginPage">Login</Link></Menu.Item>
                        <Menu.Item key="4"><Link to="/register" className="regPage">Register</Link></Menu.Item>
                        </>
                        }      
                    </Menu>
                </Header>                       
            </Layout>            
        </div> 
    )
}

export default Base;