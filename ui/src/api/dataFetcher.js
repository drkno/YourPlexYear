import exampleData from './exampleData';

const sleep = x => new Promise(resolve => setTimeout(resolve, x));

class DataFetcher {
    delay = 100;

    async getData() {
        await sleep(this.delay);
        return exampleData;
    }
}

const instance = new DataFetcher();

export default instance;