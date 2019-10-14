namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delAuthor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Authors_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Authors_Id" });
            DropColumn("dbo.Books", "Authors_Id");
            DropTable("dbo.Authors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100, unicode: false),
                        LastName = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "Authors_Id", c => c.Int());
            CreateIndex("dbo.Books", "Authors_Id");
            AddForeignKey("dbo.Books", "Authors_Id", "dbo.Authors", "Id");
        }
    }
}
