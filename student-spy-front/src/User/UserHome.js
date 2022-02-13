import React from 'react';
import { DatePicker, Image, Divider, Button, Card } from 'antd';
import userService from '../Services/userService';
import Moment from 'moment';

function UserHome() {
  const [dateStarted, setDate] = React.useState(new Date());
  const takeDateValue = (e) => {setDate(e.target.value)}

  const isoDate = Moment(dateStarted).format("YYYY-MM-DDTHH:mm:ss.ss");
  const accessT = JSON.parse(localStorage.getItem('accessToken'));

  var subData = {
    token: accessT,
    courseId: 1,
    date: isoDate + 'Z'
  }

  function startCourse() {
    userService.subscribeCourse(subData)
    .then(function (response) {
      console.log(response)
    })
    .catch(function (error) {
      console.log(error)
    })
  }

  return (
    <div className="homeContainer">
      <Card title="CS50" style={{ width: 450, height: 400 }}>
        <Image width={200}
          src="" />
        <p>Some description</p>
        <Divider />
        <DatePicker name="dateStarted" id="dateStarted" onChange={dt => setDate(dt)} />
        <Button type="primary" size={"medium"} onClick={startCourse}>
          Start
        </Button>
      </Card>
    </div>       
  );    
}

export default UserHome;