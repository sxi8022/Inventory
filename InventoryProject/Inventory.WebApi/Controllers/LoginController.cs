using Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Inventory.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// 로그인(rest방식)
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginViewModel Post(LoginViewModel loginData)
        {
            LoginViewModel model = new LoginViewModel();

            if (loginData != null)
            {
                if (loginData.userId == "admin" && loginData.passWord == "1")
                {
                    model.userId = "admin";
                    model.passWord = "1";
                    model.state = "SUCCESS";
                }
                else
                {
                    model.userId = string.Empty;
                    model.passWord = string.Empty;
                    model.state = "FAIL";
                }
            }
            else
            {
                model.userId = string.Empty;
                model.passWord = string.Empty;
                model.state = "FAIL";
            }
            return model;
        }
    }
}