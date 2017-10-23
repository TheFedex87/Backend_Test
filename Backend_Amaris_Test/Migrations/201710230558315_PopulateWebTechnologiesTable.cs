namespace Backend_Amaris_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateWebTechnologiesTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT WebTechnologies ON");
            Sql("INSERT INTO WebTechnologies (Id, Name) VALUES(1, 'PHP')");
            Sql("INSERT INTO WebTechnologies (Id, Name) VALUES(2, '.NET')");
            Sql("INSERT INTO WebTechnologies (Id, Name) VALUES(3, 'Java')");
            Sql("INSERT INTO WebTechnologies (Id, Name) VALUES(4, 'Other')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM WebTechnologies WHERE Name = 'PHP'");
            Sql("DELETE FROM WebTechnologies WHERE Name = '.NET'");
            Sql("DELETE FROM WebTechnologies WHERE Name = 'Java'");
            Sql("DELETE FROM WebTechnologies WHERE Name = 'Other'");
        }
    }
}
