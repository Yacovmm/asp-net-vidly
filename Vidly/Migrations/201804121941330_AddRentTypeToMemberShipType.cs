namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentTypeToMemberShipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberShipTypes", "RentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberShipTypes", "RentType");
        }
    }
}
