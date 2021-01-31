namespace AppDev_MCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrainerUserTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrainerUsers", "type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainerUsers", "type", c => c.String());
        }
    }
}
