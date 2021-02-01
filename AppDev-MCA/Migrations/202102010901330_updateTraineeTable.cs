namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTraineeTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TraineeCourses", name: "TrainerId", newName: "TraineeId");
            RenameIndex(table: "dbo.TraineeCourses", name: "IX_TrainerId", newName: "IX_TraineeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TraineeCourses", name: "IX_TraineeId", newName: "IX_TrainerId");
            RenameColumn(table: "dbo.TraineeCourses", name: "TraineeId", newName: "TrainerId");
        }
    }
}
