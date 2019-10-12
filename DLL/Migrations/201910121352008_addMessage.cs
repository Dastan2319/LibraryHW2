namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TakedBooks", "Books_Id1", "dbo.Books");
            DropForeignKey("dbo.TakedBooks", "Users_Id", "dbo.Users");
            DropIndex("dbo.TakedBooks", new[] { "Books_Id1" });
            DropIndex("dbo.TakedBooks", new[] { "Users_Id" });
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        authorId = c.Int(nullable: false),
                        message = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.TakedBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TakedBooks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Books_Id = c.Int(),
                        date = c.DateTime(nullable: false),
                        Books_Id1 = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.Messages");
            CreateIndex("dbo.TakedBooks", "Users_Id");
            CreateIndex("dbo.TakedBooks", "Books_Id1");
            AddForeignKey("dbo.TakedBooks", "Users_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.TakedBooks", "Books_Id1", "dbo.Books", "Id");
        }
    }
}
