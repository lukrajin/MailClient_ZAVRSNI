namespace MailClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectionEmails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        From = c.String(),
                        To = c.String(),
                        Subject = c.String(),
                        TextBody = c.String(),
                        HtmlBody = c.String(),
                        Date = c.DateTime(nullable: false),
                        OriginalFolder = c.Int(nullable: false),
                        CustomFolderName = c.String(),
                        MessageId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CollectionEmails");
        }
    }
}
