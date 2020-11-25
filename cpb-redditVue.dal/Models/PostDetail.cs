using System;

namespace cpb_redditVue.dal.Models
{
    public class PostDetail
    {
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string URL { get; set; }
        public string FromSubreddit { get; set; }
        public string ID { get; internal set; }
        public string FullName { get; internal set; }
        public string ImageURL { get; internal set; }
        public int CommentCount { get; internal set; }
        public string PermaLink { get; internal set; }
    }
}