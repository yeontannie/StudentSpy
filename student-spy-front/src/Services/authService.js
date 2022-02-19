import axios from "axios";

const URL = 'https://localhost:44348/api/Auth/';

export default class authService{
    static registerUser(model){
        return axios.post(URL + 'register', model);
    }   

    static loginUser(model){
        return axios.post(URL + 'login', model);
    }

    static logOut(){
        return axios.post(URL + 'logout');
    }

    static confirmed(model){
        return axios.put(URL + 'confirmed', model);
    }
}