using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Models;
using yellowantSDK;
using Microsoft.AspNet.Identity;
using System.Reflection;

namespace Sample.Controllers
{
    public class UserIntegrationController : Controller
    {
        private UserIntegrationDbContext db = new UserIntegrationDbContext();
        // GET: UserIntegration
        public ActionResult Integrate()
        {
            return View(db.UserIntegration.ToList());
            //return View();
        }

        [Authorize]
        public RedirectResult NewIntegration()
        {
            //UserIntegration ui = new UserIntegration();
            string ClientID = "FvbTB2WePePZH3Zz7IEEvzPpe84FSosINSG67bus";
            YellowantUserState yau = new YellowantUserState();
            yau.UserState = Guid.NewGuid().ToString();
            yau.UserUniqueID = User.Identity.GetUserId();

            using (var context = new YellowantUserStateDbContext())
            {
                context.YellowantUserState.Add(yau);
                context.SaveChanges();
            }

            string url = "https://yellowant.com/api/oauth2/authorize/?state=" + yau.UserState;
            url = url + "&client_id=" + ClientID + "&response_type=code&redirect_url="+ "http://52036862.ngrok.io/userintegration/oauthredirect";
            return Redirect(url);
        }

        [Authorize]
        public RedirectResult oauthredirect(string state, string code) 
        {

            YellowantUserStateDbContext context = new YellowantUserStateDbContext();
            var user = context.YellowantUserState.Where(a => a.UserState == state).FirstOrDefault();
           // yau.UserUniqueID = User.Identity.GetUserId();
            if (user.UserUniqueID != User.Identity.GetUserId()) {
                return Redirect("/userintegration/integrate/");
            }

            yellowant ya = new yellowant
            {
                AppKey = "FvbTB2WePePZH3Zz7IEEvzPpe84FSosINSG67bus",
                AppSecret = "6YMYY9oB9sU8imWBcYM3Z0MCjbnhCBCWbGHDICODyTLPmKXlqCeanEZrL9xNSuhZ9Eja54Mye5OfAPS2ZrJF1trT0Ag2byh31bMGXpFMQsvc2w5loBLuhmpK5q1d8HeT",
                RedirectURI = "http://52036862.ngrok.io/userintegration/oauthredirect",
                AccessToken = ""
            };
            Object AccessToken = ya.GetAccessToken(code);

            var token = ((Newtonsoft.Json.Linq.JObject)AccessToken).GetValue("access_token").ToString();
            yellowant yan = new yellowant
            {
                AccessToken = token
            };

            var user_integration = yan.CreateUserIntegration();
            return Redirect("/userintegration/integrate"); 
        }
    }
}