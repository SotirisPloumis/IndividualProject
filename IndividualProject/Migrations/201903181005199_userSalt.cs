namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userSalt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        SubmissionDate = c.DateTime(nullable: false),
                        OralMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Stream = c.String(),
                        Type = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        oralMark = c.Int(nullable: false),
                        totalMark = c.Int(nullable: false),
                        submitted = c.Boolean(nullable: false),
                        submissionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.AssignmentId, t.CourseId })
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.AssignmentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseAssignments",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Assignment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Assignment_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.Assignment_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Assignment_Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Course_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.TrainerCourses",
                c => new
                    {
                        Trainer_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trainer_Id, t.Course_Id })
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Trainer_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.TrainerCourses", "Trainer_Id", "dbo.Trainers");
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentAssignments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentAssignments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentAssignments", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.CourseAssignments", "Assignment_Id", "dbo.Assignments");
            DropForeignKey("dbo.CourseAssignments", "Course_Id", "dbo.Courses");
            DropIndex("dbo.TrainerCourses", new[] { "Course_Id" });
            DropIndex("dbo.TrainerCourses", new[] { "Trainer_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Student_Id" });
            DropIndex("dbo.CourseAssignments", new[] { "Assignment_Id" });
            DropIndex("dbo.CourseAssignments", new[] { "Course_Id" });
            DropIndex("dbo.StudentAssignments", new[] { "CourseId" });
            DropIndex("dbo.StudentAssignments", new[] { "AssignmentId" });
            DropIndex("dbo.StudentAssignments", new[] { "StudentId" });
            DropTable("dbo.TrainerCourses");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.CourseAssignments");
            DropTable("dbo.Users");
            DropTable("dbo.Trainers");
            DropTable("dbo.StudentAssignments");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Assignments");
        }
    }
}
