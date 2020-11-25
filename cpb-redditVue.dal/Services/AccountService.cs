using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.dal.Models;
using cpb_redditVue.dal.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace cpb_redditVue.dal.Services
{
    public class AccountService : RedditBaseService, IAccountService
    {
        public AccountService(IOptions<RedditConfig> config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        public AccountDetail GetLoggedInUserDetail()
        {
            return this.RedditClient.Account
                .ToRedditAccountDetail();
        }
    }
}
