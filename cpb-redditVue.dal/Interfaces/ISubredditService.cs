using cpb_redditVue.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace cpb_redditVue.dal.Interfaces
{
    public interface ISubredditService
    {
        IEnumerable<PostDetail> GetSubredditPosts(string name, Enums.PostsCategory category, string after);
        IEnumerable<PostDetail> GetSubredditPosts(string name, string searchTerm, string after);
    }
}
