namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBookID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "bookId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "bookId");
        }
    }
}
