namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Key = c.String(maxLength:250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Key);
            
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
            
            CreateTable(
                "dbo.UserForConfirmedEmails",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserForConfirmedEmails", "UserId", "dbo.Users");
            DropForeignKey("dbo.ResetPasswords", "UserId", "dbo.Users");
            DropIndex("dbo.UserForConfirmedEmails", new[] { "UserId" });
            DropIndex("dbo.ResetPasswords", new[] { "UserId" });
            DropIndex("dbo.UserForConfirmedEmails", new[] { "Key" });
            DropIndex("dbo.ResetPasswords", new[] { "Key" });
            DropTable("dbo.UserForConfirmedEmails");
            DropTable("dbo.Users");
            DropTable("dbo.ResetPasswords");
        }
    }
}
