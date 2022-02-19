import React, { useEffect } from "react";
import { Table, Space } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import userService from "../Services/userService";


function AdminHome(){
    const [tableData, setTable] = React.useState();
    const [isLoading, setLoading] = React.useState(true);

    /*const students = userService.getStudents()
        .then(function (response) {
        setTable(response.data)
        })
        .catch(function (error) {
            console.log(error)
    });*/

    useEffect(() => {
        userService.getStudents()
        .then(function (response) {
          setTable(response.data)
          setLoading(false);
        })
        .catch(function (error) {
            console.log(error)});
    }, []);

    function refreshUsers(){
        userService.getStudents()
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
            console.log(error)
        });
    }

    function deleteUser(userId){
        var deleteData = {
            userId: userId
        }

        userService.deleteUser(deleteData)
        .then(function (response) {
            console.log(response)
            refreshUsers();
          })
          .catch(function (error) {
              console.log(error)
        });
    };

    const dataSource =
    tableData.map((user) => (
        key={user.email},
        nameUser={user.name},
        lastName={user.lastName},
        age={user.age},
        email={user.email},
        registerDate={user.registerDate}
    ));
        
    const columns = [
    {
        title: 'Name',
        dataIndex: 'nameUser',
        key: 'nameUser',
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
        title: 'Action',
        key: 'action',
        render: () => (
        <Space size="middle">
            <a><EditOutlined /></a>
            <a><DeleteOutlined onClick={deleteUser()} /></a>
        </Space>
        ),
    },
    ];  

    if (isLoading) {
        return <div className="adminHome">Loading...</div>;
        }
    else{
        if(tableData == 0){
            return(
                <div className="adminHome">
                    <h3>There's no students yet</h3>
                </div>
            )
        }
        else{
            <Table columns={columns} dataSource={tableData} />
        }
    }    
}

export default AdminHome;