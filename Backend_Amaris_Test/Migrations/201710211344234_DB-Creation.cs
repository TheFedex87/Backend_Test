namespace Backend_Amaris_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebDevelopers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        DayOfBirth = c.DateTime(nullable: false),
                        YearsOfExperience = c.Int(nullable: false),
                        Comments = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebTechnologies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebDeveloperStacks",
                c => new
                    {
                        WebDeveloper_Id = c.Int(nullable: false),
                        Stack_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WebDeveloper_Id, t.Stack_Id })
                .ForeignKey("dbo.WebDevelopers", t => t.WebDeveloper_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stacks", t => t.Stack_Id, cascadeDelete: true)
                .Index(t => t.WebDeveloper_Id)
                .Index(t => t.Stack_Id);
            
            CreateTable(
                "dbo.WebTechnologyWebDevelopers",
                c => new
                    {
                        WebTechnology_Id = c.Int(nullable: false),
                        WebDeveloper_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WebTechnology_Id, t.WebDeveloper_Id })
                .ForeignKey("dbo.WebTechnologies", t => t.WebTechnology_Id, cascadeDelete: true)
                .ForeignKey("dbo.WebDevelopers", t => t.WebDeveloper_Id, cascadeDelete: true)
                .Index(t => t.WebTechnology_Id)
                .Index(t => t.WebDeveloper_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebTechnologyWebDevelopers", "WebDeveloper_Id", "dbo.WebDevelopers");
            DropForeignKey("dbo.WebTechnologyWebDevelopers", "WebTechnology_Id", "dbo.WebTechnologies");
            DropForeignKey("dbo.WebDeveloperStacks", "Stack_Id", "dbo.Stacks");
            DropForeignKey("dbo.WebDeveloperStacks", "WebDeveloper_Id", "dbo.WebDevelopers");
            DropIndex("dbo.WebTechnologyWebDevelopers", new[] { "WebDeveloper_Id" });
            DropIndex("dbo.WebTechnologyWebDevelopers", new[] { "WebTechnology_Id" });
            DropIndex("dbo.WebDeveloperStacks", new[] { "Stack_Id" });
            DropIndex("dbo.WebDeveloperStacks", new[] { "WebDeveloper_Id" });
            DropTable("dbo.WebTechnologyWebDevelopers");
            DropTable("dbo.WebDeveloperStacks");
            DropTable("dbo.WebTechnologies");
            DropTable("dbo.WebDevelopers");
            DropTable("dbo.Stacks");
        }
    }
}
