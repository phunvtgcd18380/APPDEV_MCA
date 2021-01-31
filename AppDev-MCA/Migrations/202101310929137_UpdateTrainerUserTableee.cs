namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrainerUserTableee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraineeUsers", "mainProgrammingLanguage", c => c.String());
            DropColumn("dbo.TraineeUsers", "mainProgramingLanguage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TraineeUsers", "mainProgramingLanguage", c => c.String());
            DropColumn("dbo.TraineeUsers", "mainProgrammingLanguage");
        }
    }
}
