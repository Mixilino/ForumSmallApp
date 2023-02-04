import axios, { AxiosRequestConfig } from "axios";

const config: AxiosRequestConfig = { baseURL: "https://localhost:7247/" };
const axiosInstanceTs = axios.create(config);
// axiosInstanceTs.interceptors.response.use(response => {
//     return response
// }, error => {
//     if (error.response.status === 401) {
//         alert('Unauthorized');
//     }
// })

export default axiosInstanceTs;
