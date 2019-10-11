namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        Pages = c.Int(),
                        Price = c.Int(),
                        Images = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Books", t => t.Books_Id1)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Books_Id1)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ganres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.TakedBooks", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.TakedBooks", "Books_Id1", "dbo.Books");
            DropIndex("dbo.TakedBooks", new[] { "Users_Id" });
            DropIndex("dbo.TakedBooks", new[] { "Books_Id1" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Ganres");
            DropTable("dbo.Users");
            DropTable("dbo.TakedBooks");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
