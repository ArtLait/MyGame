namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserForConfirmedEmails",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        UniqueKey = c.String(),
                        Status = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserForConfirmedEmails", "UserId", "dbo.Users");
            DropIndex("dbo.UserForConfirmedEmails", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserForConfirmedEmails");
        }
    }
}
