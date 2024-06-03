
import axios from "axios";
import https from "https";

const httpsAgent = new https.Agent({
    rejectUnauthorized: false
});
const instance = axios.create({ httpsAgent })

export default instance;