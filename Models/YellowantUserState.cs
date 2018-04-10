using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sample.Models
{
    public class YellowantUserState
    {   
        public int ID { get; set; }
        public string UserUniqueID { get; set; }
        public string UserState { get; set; }

    }
    public class YellowantUserStateDbContext : DbContext
    {
        public DbSet<YellowantUserState> YellowantUserState { get; set; }
    }
}