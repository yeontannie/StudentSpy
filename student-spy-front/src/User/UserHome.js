import React, { useEffect } from 'react';
import { DatePicker, Button, Image, Card, Col, Row } from 'antd';
import courseService from '../Services/courseService';
import Moment from 'moment';

const { Meta } = Card;

function UserHome() {
  const [coursesData, setCourses] = React.useState();
  const [dateStarted, setDate] = React.useState(new Date());
  const [isLoading, setLoading] = React.useState(true);
  const [PhotoFilePath] = React.useState('https://localhost:44348/Photos/');

  const isoDate = Moment.utc(dateStarted).toISOString();
  const accessT = JSON.parse(localStorage.getItem('accessToken'));

  function startCourse(courseId) {
    if(dateStarted == ''){
      alert('Choose the date first!')
    }
    else{
      var subData = {
        courseId: courseId,
        startDate: isoDate
      };

      courseService.subscribeCourse(subData, config)
      .then(function (response) {
        console.log(response);
        refreshCourses();
      })
      .catch(function (error) {
        console.log(error)
      });
    }
  }

  function refreshCourses(){
    courseService.getUnSubCourses(config)
    .then(function (response) {
      setCourses(response.data)
    })
    .catch(function (error) {
        console.log(error)
    });
  }

  const config = {
    headers: { Authorization: accessT }
  };

  useEffect(() => {
    courseService.getUnSubCourses(config)
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
            height={150}
            alt="no photo"
            src={PhotoFilePath+course.photoPath}
            fallback={PhotoFilePath+'default.png'}
          />
        }        
        actions={[
          <DatePicker key="dateStarted" onChange={dt => setDate(dt)}/>,
          <Button type="primary" size={"medium"} onClick={() => startCourse(course.id)}>
            Subscribe
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
}}

export default UserHome;