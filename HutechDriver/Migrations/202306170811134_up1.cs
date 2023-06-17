namespace HutechDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Contact", "CCCDorCMND", c => c.String(maxLength: 25));
            AlterColumn("dbo.tb_Contact", "DiaChi", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.tb_Contact", "Message", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Contact", "Message", c => c.String(maxLength: 4000));
            AlterColumn("dbo.tb_Contact", "DiaChi", c => c.String());
            AlterColumn("dbo.tb_Contact", "CCCDorCMND", c => c.String());
        }
    }
}
