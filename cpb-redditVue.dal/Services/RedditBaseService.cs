using cpb_redditVue.dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Reddit;

namespace cpb_redditVue.dal.Services
{
    public class RedditBaseService
    {
        private readonly IOptions<RedditConfig> config;
        private readonly RedditClient redditClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RedditBaseService(IOptions<RedditConfig> config, IHttpContextAccessor httpContextAccessor)
        {
            this.config = config;
            this.httpContextAccessor = httpContextAccessor;

            this.redditClient = new RedditClient(
                appId: this.config.Value.AppId, 
                appSecret: this.config.Value.AppSecret, 
                accessToken: this.LoggedInUserAccessToken ?? this.config.Value.DefaultAccessToken, 
                refreshToken: this.LoggedInUserRefreshToken ?? this.config.Value.DefaultRefreshToken);
        }

        public RedditClient RedditClient 
        { 
            get
            {
                return this.redditClient;
            }
        }

        protected string LoggedInUserAccessToken
        {
            get
            {
                if (this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var accessToken = this.httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
                    return accessToken;
                }

                return null;
            }
        }

        protected string LoggedInUserRefreshToken
        {
            get
            {
                var refreshToken = this.httpContextAccessor.HttpContext.GetTokenAsync("refresh_token").Result;
                return refreshToken;
            }
        }
    }
}