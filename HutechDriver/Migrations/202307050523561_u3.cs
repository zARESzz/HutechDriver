namespace HutechDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class u3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Survey", "DescriptionFull", c => c.String());
            AlterColumn("dbo.Survey", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Survey", "Link", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Survey", "Link", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Survey", "Description", c => c.String());
            DropColumn("dbo.Survey", "DescriptionFull");
        }
    }
}
