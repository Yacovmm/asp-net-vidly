namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreNameChanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreTypes", newName: "Genres");
            DropForeignKey("dbo.Movies", "GenreType_Id", "dbo.GenreTypes");
            DropIndex("dbo.Movies", new[] { "GenreType_Id" });
            DropColumn("dbo.Movies", "GenreId");
            RenameColumn(table: "dbo.Movies", name: "GenreType_Id", newName: "GenreId");
            AlterColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "GenreId");
            AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenreId" });
            AlterColumn("dbo.Movies", "GenreId", c => c.Byte());
            RenameColumn(table: "dbo.Movies", name: "GenreId", newName: "GenreType_Id");
            AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "GenreType_Id");
            AddForeignKey("dbo.Movies", "GenreType_Id", "dbo.GenreTypes", "Id");
            RenameTable(name: "dbo.Genres", newName: "GenreTypes");
        }
    }
}
