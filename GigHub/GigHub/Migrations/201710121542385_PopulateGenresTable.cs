namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'House')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Techno')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Disco')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Balearic')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
