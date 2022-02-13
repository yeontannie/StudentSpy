import React from 'react';
import 'antd/dist/antd.css';

import Register from "./Pages/Register";
import Base from './Pages/BaseLayout';
import Login from './Pages/Login';
import Home from './Pages/Home';
import Courses from './Pages/Courses';
import LogOut from './Pages/LogOut';
import ConfirmEmail from './Pages/ConfirmEmail';

import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Layout } from 'antd';

const { Content, Footer } = Layout;

function App() {
  return (    
    <div className="App">
      <BrowserRouter>
      <Layout>  
        <Base />
        <Content className="site-layout" style={{ padding: '0 50px', marginTop: 64 }}>      
          <div className="site-layout-background" style={{ padding: 24, minHeight: 500 }}>            
            <Routes>
              <Route path="/courses" element={<Courses />} /> 
              <Route path="/logout" element={<LogOut />} />
              <Route path="/" element={<Home />} />    
              <Route path="/register" element={<Register />} />
              <Route path="/login" element={<Login />} />
              <Route path="/confirm-email" element={<ConfirmEmail />} />
            </Routes>           
          </div>
         </Content> 
          <Footer style={{ textAlign: 'center' }}>StudentSpy ©2022 Created by CyberOstroh</Footer> 
        </Layout>   
      </BrowserRouter>       
    </div>
  );
}

export default App;