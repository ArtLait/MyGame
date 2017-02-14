namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResetPasswords", "Email", c => c.String());
            AlterColumn("dbo.ResetPasswords", "Key", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResetPasswords", "Key", c => c.Int(nullable: false));
            AlterColumn("dbo.ResetPasswords", "Email", c => c.Int(nullable: false));
        }
    }
}
