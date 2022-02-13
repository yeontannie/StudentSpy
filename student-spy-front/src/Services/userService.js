import axios from "axios";

const URL = 'https://localhost:44348/api/User/';

export default class userService{
    static subscribeCourse(model){
        return axios.post(URL + 'subscribe', model);
    }
}