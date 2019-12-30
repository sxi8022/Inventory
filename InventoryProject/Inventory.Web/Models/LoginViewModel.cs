using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Web.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// 사용자id
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 사용자비밀번호
        /// </summary>
        public string passWord { get; set; }

        /// <summary>
        /// 로그인상태
        /// </summary>
        public string state { get; set; }
    }
}