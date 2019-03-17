namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.StudentAssignments");
            AddPrimaryKey("dbo.StudentAssignments", new[] { "StudentId", "AssignmentId", "CourseId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StudentAssignments");
            AddPrimaryKey("dbo.StudentAssignments", new[] { "StudentId", "AssignmentId" });
        }
    }
}
