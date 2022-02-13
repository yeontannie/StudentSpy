import React from "react";
import { Table, Space } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import adminService from "../Services/adminService";


function AdminHome(){
    const [tableData, setTable] = React.useState();

    const students = adminService.getStudents()
        .then(function (response) {
        setTable(response.data)
        })
        .catch(function (error) {
            console.log(error)
    });
        
    const columns = [
    {
        title: 'Id',
        dataIndex: 'id',
        key: 'id',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.id - b.id,
    },
    {
        title: 'Name',
        dataIndex: 'name',
        key: 'name',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.name - b.name,
    },
    {
        title: 'Last Name',
        dataIndex: 'lastName',
        key: 'lastName',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.lastName - b.lastName,
    },
    {
        title: 'Age',
        dataIndex: 'age',
        key: 'age',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.age - b.age,
    },
    {
        title: 'Email',
        dataIndex: 'email',
        key: 'email',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.email - b.email,
    },
    {
        title: 'Register Date',
        key: 'registerDate',
        dataIndex: 'registerDate', 
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.registerDate - b.registerDate,         
    },
    {
        title: 'Study Date',
        key: 'studyDate',
        dataIndex: 'studyDate', 
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.studyDate - b.studyDate,     
    },
    {
        title: 'Action',
        key: 'action',
        render: (text, record) => (
        <Space size="middle">
            <a><EditOutlined /> Edit</a>
            <a><DeleteOutlined /> Delete</a>
        </Space>
        ),
    },
    ];  

    return(
        <div className="adminHome">
            <Table columns={columns} dataSource={tableData} />
        </div>
    );    
}

export default AdminHome;