using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace cpb_redditVue.app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubredditController : RedditApiController
    {
        private readonly ISubredditService subredditService;

        public SubredditController(ILogger<SubredditController> logger, ISubredditService subredditService) : base(logger)
        {
            this.subredditService = subredditService;
        }

        [HttpGet]
        [Route("{name}/posts/{category?}")]
        public IEnumerable<PostDetail> GetSubredditPosts(string name = "funny", Enums.PostsCategory category = Enums.PostsCategory.HOT, string after = "")
        {
            return this.subredditService.GetSubredditPosts(name, category, after);
        }

        [HttpGet]
        [Route("{name}/posts/search/{searchTerm}")]
        public IEnumerable<PostDetail> GetSubredditSearchPosts(string name = "funny", string searchTerm = null, string after = "")
        {
            return this.subredditService.GetSubredditPosts(name, searchTerm, after);
        }
    }
}
