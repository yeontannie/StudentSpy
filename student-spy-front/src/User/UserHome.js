import React, { useEffect } from 'react';
import { DatePicker, Button, Image, Card, Col, Row } from 'antd';
import courseService from '../Services/courseService';
import Moment from 'moment';

const { Meta } = Card;

function UserHome() {
  const [coursesData, setCourses] = React.useState();
  const [dateStarted, setDate] = React.useState(new Date());
  const [isLoading, setLoading] = React.useState(true);

  const isoDate = Moment.utc(dateStarted).toISOString();
  const accessT = JSON.parse(localStorage.getItem('accessToken'));

  function startCourse(courseId) {
    var subData = {
      courseId: courseId,
      startDate: isoDate
    };

    courseService.subscribeCourse(subData, config)
    .then(function (response) {
      console.log(response)
      window.location.reload(false);
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
            alt="no photo"
            src={course.photoPath}
            fallback=""
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