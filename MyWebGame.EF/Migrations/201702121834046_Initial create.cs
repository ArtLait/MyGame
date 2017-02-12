namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserForConfirmedEmails",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Key);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        UniqueKey = c.String(),
                        Status = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserForConfirmedEmails", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Id", "dbo.UserForConfirmedEmails");
            DropIndex("dbo.Users", new[] { "Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserForConfirmedEmails");
        }
    }
}
