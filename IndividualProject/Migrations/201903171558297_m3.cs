namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentAssignments", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentAssignments", "CourseId");
            AddForeignKey("dbo.StudentAssignments", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAssignments", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentAssignments", new[] { "CourseId" });
            DropColumn("dbo.StudentAssignments", "CourseId");
        }
    }
}
