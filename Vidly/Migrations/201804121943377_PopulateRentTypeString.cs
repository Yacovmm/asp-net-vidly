namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRentTypeString : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET RentType = 'Pay as You Go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET RentType = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET RentType = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET RentType = 'Annual' WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
