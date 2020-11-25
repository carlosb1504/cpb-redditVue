using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpb_redditVue.dal.Models
{
    public class AccountDetail
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public IEnumerable<SubredditDetail> Subreddits { get; set; }
    }
}
