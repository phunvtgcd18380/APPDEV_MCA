namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.TrainerUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Type = c.String(),
                        Telephone = c.String(),
                        WorkingPlace = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerUsers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainerCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TrainerCourses", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerUsers", new[] { "Id" });
            DropIndex("dbo.TrainerCourses", new[] { "CourseId" });
            DropIndex("dbo.TrainerCourses", new[] { "TrainerId" });
            DropTable("dbo.TrainerUsers");
            DropTable("dbo.TrainerCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
        }
    }
}
