import axios from "axios";

const URL = 'https://localhost:44348/api/Course/';

export default class courseService{
    static subscribeCourse(model, config){
        return axios.post(URL + 'subscribe', model, config);
    }

    static unsubscribeCourse(model, config){
        return axios.post(URL + 'unsubscribe', model, config);
    }

    static getSubCourses(config){
        return axios.get(URL + 'get-sub-courses', config);
    }

    static getUnSubCourses(config){
        return axios.get(URL + 'get-unsub-courses', config);
    }

    static getAllCourses(){
        return axios.get(URL + 'get-all-courses');
    }

    static deleteCourse(param){
        return axios.post(URL + 'delete-course', param);
    }

    static addCourse(model){
        return axios.post(URL + 'add-course', model);
    }

    static getCourseById(id){
        return axios.get(URL + 'get-by-id', {
            params:{
                courseId: id
            }
        });
    }

    static editCourse(model){
        return axios.post(URL + 'edit-course', model);
    }
}