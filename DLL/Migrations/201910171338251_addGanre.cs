namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGanre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ganres",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Books", "Ganre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Ganre");
            DropTable("dbo.Ganres");
        }
    }
}
