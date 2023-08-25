namespace HutechDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Adv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Link = c.String(maxLength: 500),
                        Type = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        CategoryID = c.Int(nullable: false),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.tb_Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        CategoryID = c.Int(nullable: false),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.tb_Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        PhoneNumber = c.String(nullable: false),
                        CCCDorCMND = c.String(maxLength: 25),
                        DiaChi = c.String(nullable: false, maxLength: 4000),
                        ImagePeople = c.String(),
                        ImagePapers = c.String(),
                        Message = c.String(nullable: false, maxLength: 4000),
                        IsRead = c.Int(nullable: false),
                        IsStatus = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.PriceTrip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        PriceLow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tb_Subscribe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FullName = c.String(),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false),
                        DescriptionFull = c.String(),
                        Link = c.String(nullable: false, maxLength: 1000),
                        PointSurvey = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Address = c.String(),
                        Avatar = c.String(),
                        ImageCCCD = c.String(),
                        ImageBike = c.String(),
                        Text = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        Point = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Trip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FullName = c.String(),
                        StartLocation = c.String(),
                        EndLocation = c.String(),
                        Distance = c.String(maxLength: 50),
                        Time = c.String(maxLength: 50),
                        Price = c.Decimal(precision: 18, scale: 2),
                        TimeBook = c.DateTime(),
                        OrderDate = c.DateTime(),
                        DriverBook = c.String(),
                        Status = c.String(),
                        DriverId = c.String(),
                        IsPaid = c.Boolean(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TripReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trip", t => t.Trip_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.tb_SystemSetting",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(maxLength: 4000),
                        SettingDescription = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            CreateTable(
                "dbo.tb_Driver",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 150),
                        DateofBirth = c.String(maxLength: 50),
                        Phone = c.String(),
                        Image = c.String(),
                        ImageCccd = c.String(),
                        ImageBike = c.String(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Survey", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trip", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TripReviews", "Trip_Id", "dbo.Trip");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tb_Posts", "CategoryID", "dbo.tb_Category");
            DropForeignKey("dbo.tb_News", "CategoryID", "dbo.tb_Category");
            DropIndex("dbo.TripReviews", new[] { "Trip_Id" });
            DropIndex("dbo.Trip", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Survey", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tb_Posts", new[] { "CategoryID" });
            DropIndex("dbo.tb_News", new[] { "CategoryID" });
            DropTable("dbo.tb_Driver");
            DropTable("dbo.tb_SystemSetting");
            DropTable("dbo.TripReviews");
            DropTable("dbo.Trip");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Survey");
            DropTable("dbo.tb_Subscribe");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PriceTrip");
            DropTable("dbo.Chat");
            DropTable("dbo.tb_Contact");
            DropTable("dbo.tb_Posts");
            DropTable("dbo.tb_News");
            DropTable("dbo.tb_Category");
            DropTable("dbo.tb_Adv");
        }
    }
}
