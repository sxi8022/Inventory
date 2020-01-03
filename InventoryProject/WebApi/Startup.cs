using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // API 경로 구성하는데 사용 WebApiConfig 클래스의 Register 메소드로 전달
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            // ASP.NET 웹 API를 Owin 서버 파이프 라인에 연결
            app.Use(config);
        }
    }
}
