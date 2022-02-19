import React, { useEffect } from "react";
import courseService from '../Services/courseService';
import { Button, Image, Card, Col, Row } from 'antd';
import { Modal } from 'antd';
import { ExclamationCircleOutlined } from '@ant-design/icons';


const { Meta } = Card;

function UserCourses() {
    const accessT = JSON.parse(localStorage.getItem('accessToken'));
    const [isLoading, setLoading] = React.useState(true);
    const [coursesData, setCourses] = React.useState();

    const config = {
        headers: { Authorization: accessT }
    };

    function confirm(courseid) {
        Modal.confirm({
          title: 'Confirm',
          icon: <ExclamationCircleOutlined />,
          content: 'Are you sure?',
          onOk() {
            console.log('OK');
            unsubscribe(courseid)
          },
          onCancel() {
            console.log('Cancel');
          },
        });        
    }

    function unsubscribe(courseid){
        var unsubData = {
            courseId: courseid
        }

        courseService.unsubscribeCourse(unsubData, config)
        .then(function(response) {
            console.log(response)
            window.location.reload(false);
        })
        .catch(function(error){
            console.log(error)
        });        
    }

    useEffect(() => {
        courseService.getSubCourses(config)
        .then(function (response) {
          setCourses(response.data)
          setLoading(false);
        })
        .catch(function (error) {
            console.log(error)});
    }, []);

    if (isLoading) {
    return <div className="homeContainer">Loading...</div>;
    }
    else{
        if(coursesData == 0){
            return(
                <div className="homeContainer">
                    <h3>You haven't subscribe anything yet</h3>
                </div>
            )
        }
        else{
            return (    
            <div className="homeContainer">
                <Row gutter={16}>
                {coursesData.map((course) => (       
                <Col span={8}>
                <Card
                key={course.id}
                style={{ width: 300 }}
                cover={
                    <Image
                    alt="no photo"
                    src={course.photoPath}
                    fallback=""
                    />
                }        
                actions={[
                    <Button type="primary" size={"medium"} onClick={() => confirm(course.id)}>
                    Unsubscribe
                    </Button>,
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

export default UserCourses;