import React, { useEffect } from "react";
import AdminCourses from '../Admin/AdminCourses';
import UserCourses from '../User/UserCourses';

function Courses() {
    const [role, setRole] = React.useState('');

  useEffect(() => {
    if(localStorage.getItem('userData')){
      setRole(JSON.parse(localStorage.getItem('userRole')));
    }
  })

  return (
    <div className="homeContainer">
      { role == 'Admin' ? <AdminCourses />
      : <UserCourses /> }
    </div>       
  );
}

export default Courses;