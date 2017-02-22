namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserForConfirmedEmailaddcolumnEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserForConfirmedEmails", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserForConfirmedEmails", "Email");
        }
    }
}
