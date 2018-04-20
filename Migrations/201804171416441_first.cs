namespace Sample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserIntegrations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        YellowantUserID = c.String(),
                        YellowantTeamSubdomain = c.String(),
                        IntegrationID = c.Int(nullable: false),
                        InvokeName = c.String(),
                        YellowantIntegrationToken = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.YellowantUserStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserUniqueID = c.String(),
                        UserState = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YellowantUserStates");
            DropTable("dbo.UserIntegrations");
        }
    }
}
