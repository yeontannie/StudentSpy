import React, { useEffect } from 'react';
import courseService from '../Services/courseService';
import { Button, Image, Card, Col, Row,  Modal, Form, Input, InputNumber, Space, Upload } from 'antd';
import { EditOutlined, DeleteOutlined, ExclamationCircleOutlined, AppstoreAddOutlined, UploadOutlined } from '@ant-design/icons';
import ImgCrop from 'antd-img-crop';

const { Meta } = Card;

function AdminCourses() {
    const [isLoading, setLoading] = React.useState(true);
    const [visible, setVisible] = React.useState(false);

    const [coursesData, setCourses] = React.useState();
    const [courseId, setCourseId] = React.useState();
    const [PhotoFileName, setPhotoName] = React.useState('');
    const [PhotoFilePath] = React.useState('https://localhost:44348/Photos/');
    
    const [titleText, setTitleText] = React.useState('');
    const [okBtnText, setOkBtn] = React.useState('');

    const [nameText, setName] = React.useState('');
    const [descText, setDesc] = React.useState('');
    const [durText, setDur] = React.useState();

    function confirmDelete(courseid) {
        Modal.confirm({
          title: 'Confirm',
          icon: <ExclamationCircleOutlined />,
          content: 'Are you sure?',
          onOk() {
            console.log('OK');
            deleteCourse(courseid)
          },
          onCancel() {
            console.log('Cancel');
          },
        });        
    }

    function deleteCourse(crsId){
        var deleteData = {
            courseId: crsId
        }

        courseService.deleteCourse(deleteData)
        .then(function (response) {
            console.log(response)
            refreshCourses();
          })
          .catch(function (error) {
              console.log(error)
        });
    };

    useEffect(() => {
        courseService.getAllCourses()
        .then(function (response) {
          setCourses(response.data)
          setLoading(false);
        })
        .catch(function (error) {
            console.log(error)});
    }, []);

    function refreshCourses(){
        courseService.getAllCourses()
        .then(function (response) {
          setCourses(response.data)
          setLoading(false);
        })
        .catch(function (error) {
            console.log(error)
        });
    }

    const onSave = (values) => {
        setVisible(false);

        var courseEditData = {
            name: values.name,
            description: values.description,
            duration: values.duration,
            photoPath: PhotoFileName
        }

        var model = {
            courseModel: courseEditData,
            courseId: courseId
        }

        courseService.editCourse(model)
        .then(function (response) {
            console.log(response)                      
            refreshCourses();
        })
        .catch(function (error) {
            console.log(error)
        });
    }
    
    const onCreate = (values) => {
        setVisible(false);
        var courseAddData = {
            name: values.name,
            description: values.description,
            duration: values.duration,
            photoPath: PhotoFileName
        }

        courseService.addCourse(courseAddData)
        .then(function (response) {
            console.log(response)            
            refreshCourses();
        })
        .catch(function (error) {
            console.log(error)
        });
    };

    const onCancel = () => {
        console.log('Clicked cancel button');
        setVisible(false);
    };

    const CourseCreateForm = ({ visible, onCreate, onCancel }) => {
        const [form] = Form.useForm();
        return (
          <Modal
            visible={visible}
            title={titleText}
            okText={okBtnText}
            cancelText="Cancel"
            onCancel={onCancel}
            onOk={() => {
              form
                .validateFields()
                .then((values) => {
                  form.resetFields();
                  uploadImg(values);                  
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
                {okBtnText == 'Save' && (
                    <Form.Item
                    name="name"
                    label="Name"
                    initialValue={nameText}
                    rules={[
                        {
                        required: true,
                        message: 'Please input the name of course!',
                        },
                    ]}
                    >
                    <Input />
                    </Form.Item>
                )}
                {okBtnText == 'Create' && (
                <Form.Item
                name="name"
                label="Name"
                rules={[
                    {
                    required: true,
                    message: 'Please input the name of course!',
                    },
                ]}
                >
                <Input />
                </Form.Item>)}     
                {okBtnText == 'Save' && (
                <Form.Item 
                    name="description" 
                    label="Description"
                    initialValue={descText}
                    rules={[
                        {
                        required: true,
                        message: 'Please input the description of course!',
                        },
                    ]}>
                    <Input />
                </Form.Item>)}
                {okBtnText == 'Create' && (
                <Form.Item 
                    name="description" 
                    label="Description"
                    rules={[
                        {
                        required: true,
                        message: 'Please input the description of course!',
                        },
                    ]}>
                    <Input />
                </Form.Item>)}
                {okBtnText == 'Save' && (
                <Form.Item
                    label="Duration"
                    name="duration"  
                    initialValue={durText}                                     
                    rules={[
                    {
                        required: true,
                        message: 'Please input the duration of course!',
                    },
                    ]}
                    >
                    <InputNumber
                        min={1}
                        max={40}
                        style={{ margin: '0 16px' }}
                    />
                    </Form.Item>)}
                {okBtnText == 'Create' && (
                <Form.Item
                    label="Duration"
                    name="duration"                                     
                    rules={[
                    {
                        required: true,
                        message: 'Please input the duration of course!',
                    },
                    ]}
                    >
                    <InputNumber
                        min={1}
                        max={40}
                        style={{ margin: '0 16px' }}
                    />
                </Form.Item>)}
                {okBtnText == 'Save' && (
                    <Form.Item
                    lable='Course Photo'
                    name="photoPath"
                    >
                    <ImgCrop rotate>
                    <Upload
                        action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                        onChange={onChange}
                       // fileList={fileList}
                        listType="picture"
                        type='file'           
                        >
                        <Button icon={<UploadOutlined />}>Upload</Button>
                    </Upload>
                    </ImgCrop>
                    </Form.Item>
                )}
                {okBtnText == 'Create' && (
                <Form.Item
                    lable='Course Photo'
                    name="photoPath"
                    >
                    <ImgCrop rotate width={300}>
                    <Upload
                        action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                        onChange={onChange}
                        listType="picture"
                        type='file'
                        >
                        <Button icon={<UploadOutlined />}>Upload</Button>
                    </Upload>
                    </ImgCrop>
                    </Form.Item>)}
            </Form>
          </Modal>
        );
    };

    const formData = new FormData();

    const onChange = (e) => {
        formData.append('file', e.fileList[0].originFileObj, e.fileList[0].name);
    };

    const uploadImg = (values) => {
        setVisible(false);

        courseService.saveImg(formData)
        .then(function(response){
            console.log(response.data);
            setPhotoName(response.data.toString());

            if(okBtnText == 'Create'){
                onCreate(values);
            }else{
                onSave(values);
            }          
        })
        .catch(function(error){
            console.log(error)
        })
    };

    function editCourse(id){
        setCourseId(id);
        
        courseService.getCourseById(id)
        .then(function(response){
            console.log(response);
            setName(response.data.name);
            setDesc(response.data.description);
            setDur(response.data.duration);

            setVisible(true);
            setTitleText('Edit Course');
            setOkBtn('Save'); 
        })
        .catch(function(error){
            console.log(error);
        });
    }

    function createCourse(){        
        setVisible(true);
        setTitleText('Create a New Course'); 
        setOkBtn('Create');
    }

    if (isLoading) {
    return <div className="homeContainer">Loading...</div>;
    }
    else{
        if(coursesData == 0){
            return(
                <div className="homeContainer">
                    <h3>There's no courses yet</h3>
                </div>
            )
        }
        else{
            return (    
            <div className="homeContainer">
                <Row>
                <div className="addCourseBtn">
                    <Space align="start">     
                        <Button
                            type="primary"
                            onClick={() => {
                            createCourse();
                            }}
                        > <AppstoreAddOutlined />
                            Add Course
                        </Button> 
                    </Space>
                </div> 
                <CourseCreateForm
                    visible={visible}
                    onCreate={onCreate}
                    onCancel={onCancel}
                />                
                </Row>

                <Row gutter={16}>
                {coursesData.map((course) => (       
                <Col span={8}>
                <Card
                key={course.id}
                style={{ width: 300 }}
                cover={
                    <Image
                    height={150}
                    alt="no photo"
                    src={PhotoFilePath+course.photoPath}
                    fallback={PhotoFilePath+'default.png'}
                    />
                }        
                actions={[
                    <EditOutlined key="edit" onClick={() => editCourse(course.id)} />,
                    <DeleteOutlined key="delete" onClick={() => confirmDelete(course.id)} />,                    
                ]}
                >
                <Meta
                    title={course.name}
                    description={course.description + '. Duration: ' + course.duration + ' weeks.'}
                />
                </Card>
                </Col>     
                ))}
                </Row>
            </div>       
            );   
        }
    }
}

export default AdminCourses;