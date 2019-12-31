using Inventory.Service;
using Inventory.Web.Models;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 사용자
    /// </summary>
    public class AccountController : ApiController
    {
        Query query = new Query();

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
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