const API_URL = '/api/v1/stats/';

class DataFetcher {
    async getData(year) {
        const response = await fetch(API_URL + year);
        const json = await response.json();
        return json;
    }
}

const instance = new DataFetcher();

export default instance;