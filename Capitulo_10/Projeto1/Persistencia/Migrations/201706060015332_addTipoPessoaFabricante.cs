namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTipoPessoaFabricante : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Fabricante", "TipoPessoa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fabricante", "TipoPessoa", c => c.String());
        }
    }
}
