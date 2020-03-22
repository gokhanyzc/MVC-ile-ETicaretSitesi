using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ETicaret.App_Start.Startup1))]

namespace ETicaret.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // Uygulamanızı nasıl yapılandıracağınız hakkında daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=316888 adresini ziyaret edin
            app.UseCookieAuthentication(new  Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            { 
                       
            AuthenticationType="ApplicationCookie", //hangi tip cookie ile işlem yapılacağı belirtilir.
            LoginPath=new PathString("/Account/Login") //kullanıcı yetkisi olmayan alana erişmek istediğinde giriş yapması için yönlendirdiğimiz deffault login sayfası
            });
        }
    }
}
