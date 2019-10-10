namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordForUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TakedBooks", "Books_Id", "dbo.Books");
            DropForeignKey("dbo.TakedBooks", "UserId", "dbo.Users");
            DropIndex("dbo.TakedBooks", new[] { "UserId" });
            DropIndex("dbo.TakedBooks", new[] { "Books_Id" });
            AddColumn("dbo.TakedBooks", "Books_Id1", c => c.Int());
            AddColumn("dbo.TakedBooks", "Users_Id", c => c.Int());
            AddColumn("dbo.Users", "password", c => c.String());
            CreateIndex("dbo.TakedBooks", "Books_Id1");
            CreateIndex("dbo.TakedBooks", "Users_Id");
            AddForeignKey("dbo.TakedBooks", "Books_Id1", "dbo.Books", "Id");
            AddForeignKey("dbo.TakedBooks", "Users_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TakedBooks", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.TakedBooks", "Books_Id1", "dbo.Books");
            DropIndex("dbo.TakedBooks", new[] { "Users_Id" });
            DropIndex("dbo.TakedBooks", new[] { "Books_Id1" });
            DropColumn("dbo.Users", "password");
            DropColumn("dbo.TakedBooks", "Users_Id");
            DropColumn("dbo.TakedBooks", "Books_Id1");
            CreateIndex("dbo.TakedBooks", "Books_Id");
            CreateIndex("dbo.TakedBooks", "UserId");
            AddForeignKey("dbo.TakedBooks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TakedBooks", "Books_Id", "dbo.Books", "Id");
        }
    }
}
