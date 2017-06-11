namespace Netotik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlang : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Category", newName: "Categories");
            RenameTable(name: "dbo.ContentTag", newName: "ContentTags");
            RenameColumn(table: "dbo.ContentTags", name: "Text", newName: "Name");
            AddColumn("dbo.Contents", "LanguageId", c => c.Int(nullable: false,defaultValue:1));
            AddColumn("dbo.ContentCategories", "LanguageId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.ContentTags", "LanguageId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Contents", "LanguageId");
            CreateIndex("dbo.ContentCategories", "LanguageId");
            CreateIndex("dbo.ContentTags", "LanguageId");
            AddForeignKey("dbo.ContentTags", "LanguageId", "dbo.Languages", "Id");
            AddForeignKey("dbo.ContentCategories", "LanguageId", "dbo.Languages", "Id");
            AddForeignKey("dbo.Contents", "LanguageId", "dbo.Languages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.ContentCategories", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.ContentTags", "LanguageId", "dbo.Languages");
            DropIndex("dbo.ContentTags", new[] { "LanguageId" });
            DropIndex("dbo.ContentCategories", new[] { "LanguageId" });
            DropIndex("dbo.Contents", new[] { "LanguageId" });
            DropColumn("dbo.ContentTags", "LanguageId");
            DropColumn("dbo.ContentCategories", "LanguageId");
            DropColumn("dbo.Contents", "LanguageId");
            RenameColumn(table: "dbo.ContentTags", name: "Name", newName: "Text");
            RenameTable(name: "dbo.ContentTags", newName: "ContentTag");
            RenameTable(name: "dbo.Categories", newName: "Category");
        }
    }
}
