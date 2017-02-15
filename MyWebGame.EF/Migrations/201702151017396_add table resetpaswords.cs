namespace MyWebGam.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableresetpaswords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Key = c.String(maxLength:250),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResetPasswords");
        }
    }
}
