using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sample.Models
{
    /*
     * This is example UserIntegration details you might want to save. Also incude details you get from 
     * 'GetUserProfile' method from Yellowant Class
     */ 
    public class UserIntegration
    {
        public int ID { get; set; }
        public string YellowantUserID { get; set; }
        public string YellowantTeamSubdomain { get; set; }
        public int IntegrationID { get; set; }
        public string InvokeName { get; set; }
        public string YellowantIntegrationToken { get; set; }

    }

   
}