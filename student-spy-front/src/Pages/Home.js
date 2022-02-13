import React, { useEffect } from "react";
import AdminHome from '../Admin/AdminHome';
import UserHome from '../User/UserHome';

function Home() {
  const [role, setRole] = React.useState('');

  useEffect(() => {
    if(localStorage.getItem('userData')){
      setRole(JSON.parse(localStorage.getItem('userRole')));
    }
  })

  return (
    <div className="homeContainer">
      { role == 'Admin' ? <AdminHome />
      : <UserHome /> }
    </div>       
  );
}

export default Home;