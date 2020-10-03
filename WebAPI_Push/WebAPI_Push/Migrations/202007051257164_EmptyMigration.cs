namespace WebAPI_Push.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubscriberDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        PrivateKey = c.String(maxLength: 100),
                        PublicKey = c.String(maxLength: 100),
                        Header = c.String(maxLength: 100),
                        Message = c.String(maxLength: 100),
                        URL = c.String(maxLength: 100),
                        Endpoint = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, fixedLength: true),
                        PrivateKey = c.String(maxLength: 100, fixedLength: true),
                        PublicKey = c.String(maxLength: 100, fixedLength: true),
                        Header = c.String(maxLength: 100, fixedLength: true),
                        Message = c.String(maxLength: 100, fixedLength: true),
                        URL = c.String(maxLength: 100, fixedLength: true),
                        Endpoint = c.String(maxLength: 250, fixedLength: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscribers");
            DropTable("dbo.SubscriberDatas");
        }
    }
}
