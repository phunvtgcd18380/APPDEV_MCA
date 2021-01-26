namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropTableTraineeCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainerCourses", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainerCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TrainerCourses", new[] { "TrainerId" });
            DropIndex("dbo.TrainerCourses", new[] { "CourseId" });
            DropTable("dbo.TrainerCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrainerCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainerId = c.String(nullable: false, maxLength: 128),
                        TrainerName = c.String(),
                        CourseId = c.Int(nullable: false),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TrainerCourses", "CourseId");
            CreateIndex("dbo.TrainerCourses", "TrainerId");
            AddForeignKey("dbo.TrainerCourses", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrainerCourses", "TrainerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
