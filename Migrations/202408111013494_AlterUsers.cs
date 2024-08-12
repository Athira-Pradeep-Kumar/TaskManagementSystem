namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterUsers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "LoginId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LoginId", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
