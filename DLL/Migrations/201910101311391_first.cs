namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TakedBooks", "date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "Ganre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Ganre", c => c.String());
            DropColumn("dbo.TakedBooks", "date");
        }
    }
}
