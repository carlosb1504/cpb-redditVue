using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpb_redditVue.dal.Models
{
    public class RedditConfig
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string DefaultAccessToken { get; set; }
        public string DefaultRefreshToken { get; set; }
    }
}
