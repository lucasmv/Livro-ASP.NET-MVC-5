namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTipoPessoaFabricante2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fabricante", "TipoPessoa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fabricante", "TipoPessoa");
        }
    }
}
