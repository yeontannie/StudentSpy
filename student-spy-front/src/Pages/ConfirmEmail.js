import React from "react";

function ConfirmEmail() {
    return (
        <div className="confirmEmailContainer">
            <h3>Email Confirmed Successfully!</h3>
            <p>You can <a href="http://localhost:3000/login">login</a> now!</p>
        </div>       
    );    
}

export default ConfirmEmail;