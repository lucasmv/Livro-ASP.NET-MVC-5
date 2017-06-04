namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValorUnitarioProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "ValorUnitario", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "ValorUnitario");
        }
    }
}
