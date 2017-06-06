namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusFabricante : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fabricante", "EstaAtivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fabricante", "EstaAtivo");
        }
    }
}
