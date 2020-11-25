using cpb_redditVue.dal.Models;
using Reddit.Controllers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace cpb_redditVue.dal.Helpers
{
    public static class RedditDotNetAccountExtensionMethods
    {
        public static AccountDetail ToRedditAccountDetail(this Account redditDotNetAccount)
        {
            return new AccountDetail
            {
                FullName = redditDotNetAccount.Me.Fullname,
                Username = redditDotNetAccount.Me.Name,
                Subreddits = redditDotNetAccount
                                .MySubscribedSubreddits()
                                .ToSubRedditDetailList()
            };
        }
    }

    public static class RedditDotNetSubRedditExtensionMethods
    {
        public static IEnumerable<SubredditDetail> ToSubRedditDetailList(this List<Subreddit> subredditList)
        {
            return subredditList.Select(subreddit => new SubredditDetail
            {
                Name = subreddit.Name
            });
        }
    }
    public static class RedditDotNetPostExtensionMethods
    {
        public static IEnumerable<PostDetail> ToPostDetailList(this IEnumerable<Post> redditDotNetPostList)
        {
            return redditDotNetPostList.Select(post =>
            {

                var newPostDetail = new PostDetail
                {
                    ID = post.Id,
                    FromSubreddit = post.Subreddit,
                    Title = post.Title,
                    DateCreated = post.Created,
                    UserCreated = post.Author,
                    UpVotes = post.UpVotes,
                    DownVotes = post.DownVotes,
                    URL = post.Listing.URL,
                    FullName = post.Fullname,
                    CommentCount = post.Listing.NumComments,
                    PermaLink = $"https://www.reddit.com{post.Listing.Permalink}"
                };

                try
                {
                    newPostDetail.ImageURL = post.Listing.Preview["images"][0]["source"].Value<string>("url");
                }
                catch (NullReferenceException)
                {
                    // Perhaps the post is text only. set the URL to null.
                    newPostDetail.ImageURL = null;
                }

                return newPostDetail;

            });
        }
    }
}
