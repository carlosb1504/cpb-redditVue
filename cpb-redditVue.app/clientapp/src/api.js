import axios from 'axios';

const client = axios.create({
    json: true,
});

function errHandler(error) {
    if (error.response) {
        // Request made and server responded
        console.log(error.response.data);
        console.log(error.response.status);
        console.log(error.response.headers);
    } else if (error.request) {
        // The request was made but no response was received
        console.log(error.request);
    } else {
        // Something happened in setting up the request that triggered an Error
        console.log('Error', error.message);
    }
}

export default {
    async execute(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
        })
            .then(req => req.data)
            .catch(errHandler);
    },
    getPostsByCategory(sub, cat, after) {
        let url = `/subreddit/${sub}/posts/${cat}?after=${after}`

        return this.execute('get', url);
    },
    getPostsBySearch(sub, search, after) {
        let url = `/subreddit/${sub}/posts/search/${search}?after=${after}`

        return this.execute('get', url);
    },
    getAccountDetail() {
        return this.execute('get', `/account`);
    },
};