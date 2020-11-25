<template>
    <div class="postList">
        <b-navbar class="py-1" type="dark" fixed="top" variant="dark" toggleable="lg" sticky>
            <b-navbar-brand id="navbar-brand" href="#">{{ title }}</b-navbar-brand>
            <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
            <b-collapse id="nav-collapse" is-nav>
                <b-navbar-nav>
                    <b-nav-form @submit.prevent="doSearch">
                        <!--<button v-on:click="getMorePosts">get more</button>-->
                        <label class="mr-sm-2" for="subDropDown">Viewing:</label>
                        <b-form-select id="subDropDown" v-model="selectedSub" :options="subredditSelectList" @change="refresh">
                            <b-form-select-option value="earthporn">r/earthporn</b-form-select-option>
                        </b-form-select>
                        <label class="mr-sm-2" for="subDropDown">Filter:</label>
                        <b-form-select v-model="currentFilter" @change="doFilterChange" :options="filterList"></b-form-select>
                        <label class="mr-sm-2" for="subDropDown">Or Search:</label>
                        <b-form-input v-model="currentSearchTerm"></b-form-input>
                        <b-button variant="primary" @click="doSearch">Go</b-button>
                        <b-button variant="secondary" @click="resetSearch">Reset</b-button>
                    </b-nav-form>
                </b-navbar-nav>
                <b-navbar-nav class="ml-auto">
                    <div v-show="loggedInUsername == ''">
                        <a href="/signin">Log In to view your subs!</a>
                    </div>
                    <div v-show="loggedInUsername != ''"><a href="/signout">{{ loggedInUsername }} Logged In (Log Out)</a></div>
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>
        
        <b-alert :show="loading" variant="secondary">Loading...</b-alert>
        <b-alert :show="error" variant="danger">An error has occurred</b-alert>

        <div v-infinite-scroll="getMorePosts" infinite-scroll-disabled="loading" infinite-scroll-distance="10">
            <div class="grid">
                <!-- items -->
                <a v-bind:title="post.title"
                   v-for="post in postCollection"
                   :key="post.ID"
                   :href="post.permaLink"
                   target="_blank"
                   class="item">
                    <div class="content">
                        <img v-bind:src="post.imageURL" @load="imageLoad" @error="imageLoad" />
                        <div class="desc">
                            <div class="description">
                                <p>{{post.title}}</p>
                            </div>
                            <div class="meta">
                                u/{{post.userCreated}} on {{post.dateCreated | formatDate}} | <b-icon-arrow-up></b-icon-arrow-up>{{post.upVotes}} <b-icon-arrow-down></b-icon-arrow-down>{{post.downVotes}} | {{post.commentCount}} comments
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
</template>

<script>
    import layoutUtils from './../layoutUtils';
    import api from './../api';

    export default {
        name: 'PostList',
        props: {
            title: String
        },
        data: function () {
            return {
                postCollection: [],
                loading: false,
                error: false,
                currentFilter: "TOP",
                currentSearchTerm: "",
                subredditList: [],
                selectedSub: 'earthporn',
                loggedInUsername: '',
                filterList: [
                    { value: "HOT", text: "HOT" },
                    { value: "NEW", text: "NEW" },
                    { value: "TOP", text: "TOP" },
                    { value: "RISING", text: "RISING" },
                    { value: "CONTROVERSIAL", text: "CONTROVERSIAL" },
                    { value: "SEARCH", text: "SEARCH" },
                ]
            }
        },
        computed: {
            lastPostFullname: function () {
                if (this.postCollection.length > 0) {
                    return this.postCollection[this.postCollection.length - 1].fullName
                }
                return "";
            },
            subredditSelectList: function () {
                var subredditSelectListArray = [];

                if (this.subredditList.length > 0) {
                    for (var i = 0; i < this.subredditList.length; i++) {
                        subredditSelectListArray.push({ value: this.subredditList[i].name, text: 'r/' + this.subredditList[i].name });
                    }
                }

                return subredditSelectListArray;

            }
        },
        methods: {
            getAccountInfo: async function () {
                this.loading = true;
                const accDetail = await api.getAccountDetail();
                this.subredditList = accDetail.subreddits;
                this.loggedInUsername = (accDetail.username == 'redditVueApp' ? '' : accDetail.username);
                this.loading = false;
            },
            getMorePosts: async function () {

                if (this.loading)
                    return;

                // Don't load any more than 100 for now as things start to get a little slow. Some optimization required.
                if (this.postCollection.length >= 100) {
                    return;
                }

                this.loading = true;

                let newPosts = [];
                if (this.currentFilter == "SEARCH") {
                    newPosts = await api.getPostsBySearch(this.selectedSub, this.currentSearchTerm, this.lastPostFullname);
                }
                else {
                    newPosts = await api.getPostsByCategory(this.selectedSub, this.currentFilter, this.lastPostFullname);
                }
                this.postCollection = [...this.postCollection, ...newPosts];
                this.loading = false;

            },
            async doFilterChange() {
                this.currentSearchTerm = '';
                this.refresh();
            },
            async doSearch() {
                this.currentFilter = "SEARCH";
                this.refresh();
            },
            async resetSearch() {
                this.currentFilter = "TOP";
                this.currentSearchTerm = '';
                this.refresh();
            },
            async refresh() {
                this.clearPosts();
                await this.getMorePosts();
            },
            clearPosts() {
                this.postCollection = [];
            },
            imageLoad() {
                this.resizeGrid();
            },
            resizeGrid() {
                layoutUtils.resizeAllGridItems();
            },
            errorHandler() {
                this.loading = false;
                this.error = true;
            }
                
        },
        async created() {
            await this.getAccountInfo();
            await this.getMorePosts();
        },
        mounted() {
            window.addEventListener("resize", this.resizeGrid);
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    img {
        vertical-align: middle;
        max-width: 100%;
    }

    .item {
        background-color: #2d2d2d;
        border-radius: 0.3rem;
        overflow: hidden;
    }

    .desc {
        padding: 0.5rem;
    }

    a, a:hover {
        color: inherit;
    }

    .grid a:hover {
        text-decoration: none;
    }

    .grid {
        display: grid;
        grid-gap: 10px;
        grid-template-columns: repeat(auto-fill, minmax(400px,1fr));
        grid-auto-rows: 20px;
    }

    .meta {
        color: #737373;
        font-size: 0.9em;
    }

</style>
