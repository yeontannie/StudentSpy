function LogOut() {
    async function handleLogOut(){
        localStorage.clear();
        window.location.href = '/login';
    }

    handleLogOut();    
}

export default LogOut;