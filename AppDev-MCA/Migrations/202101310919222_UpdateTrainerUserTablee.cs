namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrainerUserTablee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainerUsers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainerUsers", "FullName");
        }
    }
}
