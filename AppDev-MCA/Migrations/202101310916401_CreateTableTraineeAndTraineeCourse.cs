namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTraineeAndTraineeCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainerId = c.String(nullable: false, maxLength: 128),
                        TraineeName = c.String(),
                        CourseId = c.Int(nullable: false),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.TraineeUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FullName = c.String(),
                        age = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Telephone = c.String(),
                        mainProgramingLanguage = c.String(),
                        ToeicScore = c.String(),
                        Department = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeUsers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TraineeCourses", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeUsers", new[] { "Id" });
            DropIndex("dbo.TraineeCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeCourses", new[] { "TrainerId" });
            DropTable("dbo.TraineeUsers");
            DropTable("dbo.TraineeCourses");
        }
    }
}
