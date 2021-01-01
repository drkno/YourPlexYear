const API_URL = '/api/v1/stats/2020';

class DataFetcher {
    async getData() {
        const response = await fetch(API_URL);
        const json = await response.json();
        return json;
    }
}

const instance = new DataFetcher();

export default instance;