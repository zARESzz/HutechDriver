namespace HutechDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class u2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageCCCD", c => c.String());
            AddColumn("dbo.AspNetUsers", "ImageBike", c => c.String());
            AddColumn("dbo.AspNetUsers", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Text");
            DropColumn("dbo.AspNetUsers", "ImageBike");
            DropColumn("dbo.AspNetUsers", "ImageCCCD");
        }
    }
}
