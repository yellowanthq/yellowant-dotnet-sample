using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Models;
using Sample.CommandCenter;
using yellowantSDK;
using Microsoft.AspNet.Identity;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net;


namespace Sample.Controllers
{
    public class JSONNetResult : ActionResult
    {
        private readonly JObject _data;
        public JSONNetResult(JObject data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.Write(_data.ToString(Newtonsoft.Json.Formatting.None));
        }
    }
    /*
     * This is an example Controller class. You might want to define different controllers for handling
     * userintegrations, settings and api calls respectively. Also make sure AntiForgeryToken is applied 
     * wherever necessery 
     */ 

    public class UserIntegrationController : Controller
    {

        private DefaultConnectionContext db = new DefaultConnectionContext();
        // GET: UserIntegration
        public ActionResult Integrate()
        {
            return View(db.UserIntegrationContext.ToList());
        }

        [Authorize]
        public RedirectResult NewIntegration()
        {
            string ClientID = "Rvpy9UMwy4wfOR0HHohOoCdXk3IIl2n4BhzpMC28";
            YellowantUserState yau = new YellowantUserState();
            yau.UserState = Guid.NewGuid().ToString();
            yau.UserUniqueID = User.Identity.GetUserId();

            db.YellowantUserStatesContext.Add(yau);
            db.SaveChanges();

            string url = "http://company_domain.yellowant.com/api/oauth2/authorize/?state=" + yau.UserState;
            url = url + "&client_id=" + ClientID + "&response_type=code&redirect_url="+ "http://appurl.com/userintegration/oauthredirect";
            return Redirect(url);
        }

        [Authorize]
        public ActionResult oauthredirect(string state, string code) 
        {
            var user = db.YellowantUserStatesContext.Where(a => a.UserState == state).FirstOrDefault();
            if (user.UserUniqueID != User.Identity.GetUserId()) {
                return Redirect("http://appurl.com/userintegration/integrate/");
            }

            Yellowant ya = new Yellowant
            {
                AppKey = "Rvpy9UMwy4wfOR0HHohOoCdXk3IIl2n4BhzpMC28",
                AppSecret = "9ZjwhuaBmFS1Sq08QHDkWay9pBbLYXsbMRSBzOXV6pj7pCILsWDNHl3vfChwgnD90KpNqEJxottOlJp5esefQGVFRKwq13hgrVO2iKDADhpjFg8nytgyeSEC43ikl0Uq",
                RedirectURI = "http://appurl.com/userintegration/oauthredirect",
                AccessToken = ""
            };
            dynamic AccessToken = ya.GetAccessToken(code);

            string token = AccessToken.access_token;
            Yellowant yan = new Yellowant
            {
                AccessToken = token
            };

            dynamic user_integration = yan.CreateUserIntegration();
            dynamic user_profile = yan.GetUserProfile();
            UserIntegration integration = new UserIntegration {
                YellowantUserID = user.UserUniqueID,
                IntegrationID = user_integration["user_application"],
                InvokeName = user_integration["user_invoke_name"],
                YellowantIntegrationToken = token,
                YellowantTeamSubdomain = "temp"
            };

            db.UserIntegrationContext.Add(integration);
            db.SaveChanges();
            return Redirect("http://appurl.com/userintegration/integrate/"); 
        }

        [HttpPost]
        public ActionResult Api()
        {
            var data = Request.Form["data"];

            dynamic command = JsonConvert.DeserializeObject(data);
            if (command["verification_token"] != "IUxMxNmPJkrLTTuS5TL7aDtadJFmt2QlyjHSG74aOHdFmeL98DP31l0vAGqjY3dd4KU6kEY6M17SYmrsl6Q2rKTA4PrniAgrMOVybfYJtwaW8q2FhT49L9qIkbXx3KBD")
            {
                JObject er = new JObject();
                er.Add("Error", "Wrong Verification");
                return new JSONNetResult(er);
            }

            Commands cmd = new Commands()
            {
                FunctionName = command["function_name"],
                FunctionID = command["function"],
                IntegrationID = command["application"],
                Args = command["args"]
            };
            string result = cmd.Process();

            return this.Content(result, "application/json");
        }

        
        
    }
}