namespace PartyInvites.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestResponse",
                c => new
                    {
                        GuestResponseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        WillAttend = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GuestResponseID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GuestResponse");
        }
    }
}
