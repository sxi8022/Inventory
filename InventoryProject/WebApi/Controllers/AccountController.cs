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
        public LoginViewModel Post(string userid, string password)
        {
            LoginViewModel model = new LoginViewModel();

            if (userid == "admin" && password == "1")
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

            return model;
        }
    }
}