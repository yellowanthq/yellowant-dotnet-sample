using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sample.Models
{
    public class UserIntegration
    {
        public int ID { get; set; }
        public int YellowantUserID { get; set; }
        public string YellowantTeamSubdomain { get; set; }
        public int IntegrationID { get; set; }
        public string InvokeName { get; set; }
        public string YellowantIntegrationToken { get; set; }

    }

    public class UserIntegrationDbContext : DbContext
    {
        public DbSet<UserIntegration> UserIntegration { get; set; }
    }
}