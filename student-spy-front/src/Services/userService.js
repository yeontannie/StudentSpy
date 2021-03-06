import axios from "axios";

const URL = 'https://localhost:44348/api/User/';

export default class userService{
    static getStudents(){
        return axios.get(URL + 'get-students');
    }

    static addCourse(model){
        return axios.post(URL + 'add-course', model);
    }

    static deleteUser(model){
        return axios.post(URL + 'delete-user', model);        
    }

    static editStudent(model){
        return axios.post(URL + 'edit-user', model);
    }
}