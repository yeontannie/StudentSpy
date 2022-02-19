import React, { useEffect } from "react";
import authService from "../Services/authService";

function Confirmed() {
    const [isLoading, setLoading] = React.useState(true);
    var url = window.location.href.split('/').lastIndex;

    useEffect(() => {
        authService.confirmed(url)
        .then(function(response){
            console.log(response)
            setLoading(false)
        })
        .catch(function(error){
            console.log(error)
        });
    });

    if (isLoading) {
        return <div className="confirmedContainer">Loading...</div>;
        }
    else{
    return (
        <div className="confirmedContainer">
            <h3>Email Confirmed Successfully!</h3>
            <p>You can <a href="http://localhost:3000/login">login</a> now!</p>
        </div>       
    );  }  
}

export default Confirmed;