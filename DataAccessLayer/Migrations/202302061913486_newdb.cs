namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Coach_Id", "dbo.Coaches");
            DropIndex("dbo.Teams", new[] { "Coach_Id" });
            RenameColumn(table: "dbo.Teams", name: "Coach_Id", newName: "CoachId");
            AlterColumn("dbo.Teams", "CoachId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "CoachId");
            AddForeignKey("dbo.Teams", "CoachId", "dbo.Coaches", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "CoachId", "dbo.Coaches");
            DropIndex("dbo.Teams", new[] { "CoachId" });
            AlterColumn("dbo.Teams", "CoachId", c => c.Int());
            RenameColumn(table: "dbo.Teams", name: "CoachId", newName: "Coach_Id");
            CreateIndex("dbo.Teams", "Coach_Id");
            AddForeignKey("dbo.Teams", "Coach_Id", "dbo.Coaches", "Id");
        }
    }
}
