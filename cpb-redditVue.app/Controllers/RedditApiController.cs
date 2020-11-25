using cpb_redditVue.dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cpb_redditVue.app.Controllers
{
    public abstract class RedditApiController : ControllerBase
    {
        private ILogger _logger;

        public RedditApiController(ILogger logger)
        {
            this._logger = logger;
        }

    }
}