namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberShipType1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberShipTypes", "SignUpFeee", c => c.Short(nullable: false));
            DropColumn("dbo.MemberShipTypes", "SignUpFree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberShipTypes", "SignUpFree", c => c.Short(nullable: false));
            DropColumn("dbo.MemberShipTypes", "SignUpFeee");
        }
    }
}
