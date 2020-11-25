using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.dal.Models;
using cpb_redditVue.dal.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace cpb_redditVue.dal.Services
{
    public class SubredditService : RedditBaseService, ISubredditService
    {
        public SubredditService(IOptions<RedditConfig> config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        public IEnumerable<PostDetail> GetSubredditPosts(string name, Enums.PostsCategory category = Enums.PostsCategory.TOP, string after = "")
        {
            // Get the named subreddit
            var subReddit = this.RedditClient.Subreddit(name);

            IEnumerable<PostDetail> posts;

            // Get the requested category of posts, 'after' is the id of the last post we saw, for pagination purposes.
            switch (category)
            {
                case Enums.PostsCategory.HOT:
                    posts = subReddit.Posts
                        .GetHot(after: after, limit: 20)
                        .ToPostDetailList();
                    break;
                case Enums.PostsCategory.NEW:
                    posts = subReddit.Posts
                        .GetNew(after: after, limit: 20)
                        .ToPostDetailList();
                    break;
                case Enums.PostsCategory.RISING:
                    posts = subReddit.Posts
                        .GetRising(after: after, limit: 20)
                        .ToPostDetailList();
                    break;
                case Enums.PostsCategory.CONTROVERSIAL:
                    posts = subReddit.Posts
                        .GetControversial(after: after, limit: 20)
                        .ToPostDetailList();
                    break;
                case Enums.PostsCategory.TOP:
                default:
                    posts = subReddit.Posts
                        .GetTop(after: after, limit: 20)
                        .ToPostDetailList();
                    break;
            }

            return posts;
        }

        public IEnumerable<PostDetail> GetSubredditPosts(string name, string searchTerm, string after = "")
        {
            return this.RedditClient.Subreddit(name)
                .Search(q: searchTerm, after: after, limit: 20)
                .ToPostDetailList();
        }
    }
}
