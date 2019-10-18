namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delSomeThing : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Ganres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ganres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
