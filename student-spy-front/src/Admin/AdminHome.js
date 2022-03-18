import React, { useEffect } from "react";
import { Table, Space, Modal, Form, Input, InputNumber, Button } from 'antd';
import { EditOutlined, DeleteOutlined, ExclamationCircleOutlined, SearchOutlined } from '@ant-design/icons';
import userService from "../Services/userService";
import Highlighter from 'react-highlight-words';

function AdminHome(){
    const [tableData, setTable] = React.useState();
    const [courseData, setCourses] = React.useState();
    const [isLoading, setLoading] = React.useState(true);
    const [visible, setVisible] = React.useState(false);

    const [studentName, setStudentName] = React.useState();
    const [studentNum, setStudentNum] = React.useState();
    const [pag] = React.useState({ total: studentNum, defaultPageSize: 3, defaultCurrent: 1 });

    const [nameTxt, setName] = React.useState('');
    const [lastNameTxt, setLastName] = React.useState('');
    const [ageTxt, setAge] = React.useState();

    const [searchText, setSearchText] = React.useState('');
    const [searchedColumn, setSearchedColumn] = React.useState('');
    const [searchInput, setSearchInput] = React.useState('');

    useEffect(() => {
        userService.getStudents()
        .then(function (response) {
            console.log(response);
            setTable(response.data.students.result);
            setCourses(response.data.courses);
            setStudentNum(response.data.length);
            setLoading(false);
        })
        .catch(function (error) {
            console.log(error)});
    }, []);

    function refreshUsers(){
        userService.getStudents()
        .then(function (response) {
            console.log(response)
            setTable(response.data.students.result);
            setCourses(response.data.courses);
            setStudentNum(response.data.length);
        })
        .catch(function (error) {
            console.log(error)
        });
    };

    function confirmDelete(userid) {
        Modal.confirm({
          title: 'Confirm',
          icon: <ExclamationCircleOutlined />,
          content: 'Are you sure?',
          onOk() {
            console.log('OK');
            deleteUser(userid)
          },
          onCancel() {
            console.log('Cancel');
          },
        });        
    };

    function deleteUser(student){
        var deleteData = {
            user: student
        }

        userService.deleteUser(deleteData)
        .then(function (response) {
            console.log(response)
            if(response.data.includes("Can't")){
                alert(response.data);
                refreshUsers();
            }            
          })
          .catch(function (error) {
              console.log(error)
        });
    };

    const getFullDate = (date) => {
        const dateAndTime = date.split('T');
        return dateAndTime[0].split('-').reverse().join('-');
    };

    function editStudent(student){
        const name = student.email.split('@');
        setStudentName(name[0]);

        setName(student.name);        
        setLastName(student.lastName);        
        setAge(student.age);
        
        setVisible(true);
    };

    const onCancel = () => {
        console.log('Clicked cancel button');
        setVisible(false);
    };

    const onSave = (values) => {
        setVisible(false);

        const model = {
            name: values.name,
            lastName: values.lastName,
            age: values.age,
            userName: studentName
        }

        userService.editStudent(model)
        .then(function (response) {
            console.log(response)                      
            refreshUsers();
        })
        .catch(function (error) {
            console.log(error)
        });
    };

    const UserEditForm = ({ visible, onCancel }) => {
        const [form] = Form.useForm();
        return (
          <Modal
            visible={visible}
            title='Edit User'
            okText='Save'
            cancelText='Cancel'
            onCancel={onCancel}
            onOk={() => {
              form
                .validateFields()
                .then((values) => {
                  form.resetFields(); 
                  onSave(values);                 
                })
                .catch((info) => {
                  console.log('Validate Failed:', info);
                });
            }}
          >
            <Form
              form={form}
              layout="vertical"
              name="form_in_modal"
              initialValues={{
                modifier: 'public',
              }}
            >
                <Form.Item
                name="name"
                label="Name"
                initialValue={nameTxt}
                rules={[
                    {
                    required: true,
                    message: 'Please input the name of user!',
                    },
                ]}
                >
                <Input />
                </Form.Item>
    
                <Form.Item 
                    name="lastName" 
                    label="Last Name"
                    initialValue={lastNameTxt}
                    rules={[
                        {
                        required: true,
                        message: 'Please input the lastname of user!',
                        },
                    ]}>
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Age"
                    name="age"  
                    initialValue={ageTxt}                                     
                    rules={[
                    {
                        required: true,
                        message: 'Please input the age of user!',
                    },
                    ]}
                    >
                <InputNumber
                    min={1}
                    max={40}
                    style={{ margin: '0 16px' }}
                />
                </Form.Item>
            </Form>
          </Modal>
        );
    };

    const getColumnSearchProps = (dataIndex) => ({
        filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters }) => (
        <div style={{ padding: 8 }}>
            <Input
            ref={node => {
                setSearchInput(node);
            }}
            placeholder={`Search ${dataIndex}`}
            value={selectedKeys[0]}
            onChange={e => setSelectedKeys(e.target.value ? [e.target.value] : [])}
            onPressEnter={() => handleSearch(selectedKeys, confirm, dataIndex)}
            style={{ marginBottom: 8, display: 'block' }}
            />
            <Space>
            <Button
                type="primary"
                onClick={() => handleSearch(selectedKeys, confirm, dataIndex)}
                icon={<SearchOutlined />}
                size="small"
                style={{ width: 90 }}
            >
                Search
            </Button>
            <Button onClick={() => handleReset(clearFilters)} size="small" style={{ width: 90 }}>
                Clear
            </Button>
            </Space>
        </div>
        ),
        filterIcon: filtered => <SearchOutlined style={{ color: filtered ? '#1890ff' : undefined }} />,
        onFilter: (value, record) =>
        record[dataIndex]
            ? record[dataIndex].toString().toLowerCase().includes(value.toLowerCase())
            : '',
        onFilterDropdownVisibleChange: visible => {
        if (visible) {
            setTimeout(() => searchInput.select(), 100);
        }
        },
        render: text =>
        searchedColumn === dataIndex ? (
            <Highlighter
            highlightStyle={{ backgroundColor: '#ffc069', padding: 0 }}
            searchWords={[searchText]}
            autoEscape
            textToHighlight={text ? text.toString() : ''}
            />
        ) : (
            text
        ),
    });
    
    const handleSearch = (selectedKeys, confirm, dataIndex) => {
        confirm();
        setSearchText(selectedKeys[0]);
        setSearchedColumn(dataIndex);
    };
    
    const handleReset = (clearFilters) => {
        clearFilters(); 
        refreshUsers();       
        setSearchText('');
    };
        
    const columns = [
    {
        title: 'Name',
        dataIndex: 'name',
        key: 'name',
        defaultSortOrder: 'descend',
        //sorter: { ...sortUsers('name') },
        sorter: (a, b) => a.name.length - b.name.length,
        ...getColumnSearchProps('name'),
    },
    {
        title: 'Last Name',
        dataIndex: 'lastName',
        key: 'lastName',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.lastName.length - b.lastName.length,
        ...getColumnSearchProps('lastName'),
    },
    {
        title: 'Age',
        dataIndex: 'age',
        key: 'age',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.age - b.age,
        ...getColumnSearchProps('age'),
    },
    {
        title: 'Email',
        dataIndex: 'email',
        key: 'email',
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.email.length - b.email.length,
        ...getColumnSearchProps('email'),
    },
    {
        title: 'Register Date',
        dataIndex: 'registerDate',
        key: 'registerDate', 
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.registerDate - b.registerDate,
        ...getColumnSearchProps('registerDate'),
        render: ((date) => getFullDate(date)),              
    },
    {
        title: 'Action',
        key: 'action',
        render: (student) => (
        <Space size="middle">
            <a><EditOutlined key="edit" onClick={() => editStudent(student)} /></a>
            <a><DeleteOutlined key="dlt" onClick={() => confirmDelete(student)} style={{ color: "red"}} /></a>
        </Space>
        ),
    },
    ];  

    if (isLoading) {
        return( <div className="adminHome">Loading...</div>
        )}
    else{
        if(tableData == 0){
            return(
                <div className="adminHome">
                    <h3>There's no students yet</h3>
                </div>
            )
        }
        else{
            return(
                <div>
                    <UserEditForm
                        visible={visible}
                        onSave={onSave}
                        onCancel={onCancel}
                    />
                    <Table columns={columns} dataSource={tableData}
                    pagination={pag} rowKey={row => row.id}
                    expandable={{
                        expandedRowRender: record => <p style={{ margin: 0, whiteSpace: "pre-line" }}>{courseData[record.email].map((course) => (
                            course.name + '. ' + course.duration + ' weeks. ' + course.description + '.\n'))}</p>,
                        rowExpandable: record => courseData.hasOwnProperty(record.email),
                    }} />
                </div>
            )
        }
    }    
}

export default AdminHome;