using cpb_redditVue.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace cpb_redditVue.dal.Interfaces
{
    public interface IAccountService
    {
        AccountDetail GetLoggedInUserDetail();
    }
}
