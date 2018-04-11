namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberShipType2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberShipTypes", "SignUpFee", c => c.Short(nullable: false));
            DropColumn("dbo.MemberShipTypes", "SignUpFeee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberShipTypes", "SignUpFeee", c => c.Short(nullable: false));
            DropColumn("dbo.MemberShipTypes", "SignUpFee");
        }
    }
}
