using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cpb_redditVue.app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : RedditApiController
    {
        private readonly IAccountService accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService) : base(logger)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public AccountDetail Get()
        {
            return this.accountService.GetLoggedInUserDetail();
        }
    }
}
