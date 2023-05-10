namespace HutechDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 255),
                        Gmail = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 50),
                        DateCreate = c.DateTime(),
                        Writing = c.String(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chat");
        }
    }
}
