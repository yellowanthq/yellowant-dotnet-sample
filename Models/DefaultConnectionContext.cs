﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Sample.Models
{
    public class DefaultConnectionContext : DbContext
    {
        public DefaultConnectionContext(): base("DefaultConnection")
        {

        }

        public DbSet<UserIntegration> UserIntegrationContext {get; set;}
        public DbSet<YellowantUserState> YellowantUserStatesContext { get; set; }
    }
}