namespace MailClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomFoldermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomFolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                        ItemCount = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomFolders");
        }
    }
}
