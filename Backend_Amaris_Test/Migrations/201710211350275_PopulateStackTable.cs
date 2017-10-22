namespace Backend_Amaris_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStackTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Stacks ON");
            Sql("INSERT INTO Stacks (Id, Name) VALUES(1, 'FrontEnd')");
            Sql("INSERT INTO Stacks (Id, Name) VALUES(2, 'BackEnd')");
            Sql("INSERT INTO Stacks (Id, Name) VALUES(3, 'FullStack')");
            Sql("INSERT INTO Stacks (Id, Name) VALUES(4, 'DevOps')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Stacks WHERE Name = 'FrontEnd'");
            Sql("DELETE FROM Stacks WHERE Name = 'BackEnd'");
            Sql("DELETE FROM Stacks WHERE Name = 'FullStack'");
            Sql("DELETE FROM Stacks WHERE Name = 'DevOps'");
        }
    }
}
