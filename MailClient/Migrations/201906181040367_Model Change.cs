namespace MailClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CollectionEmails", "MessageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectionEmails", "MessageId", c => c.String());
        }
    }
}
