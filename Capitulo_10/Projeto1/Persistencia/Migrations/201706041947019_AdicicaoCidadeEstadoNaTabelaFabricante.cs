namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicicaoCidadeEstadoNaTabelaFabricante : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fabricante", "EstadoID", c => c.Long());
            AddColumn("dbo.Fabricante", "CidadeID", c => c.Long());
            CreateIndex("dbo.Fabricante", "EstadoID");
            CreateIndex("dbo.Fabricante", "CidadeID");
            AddForeignKey("dbo.Fabricante", "CidadeID", "dbo.Cidade", "CidadeID");
            AddForeignKey("dbo.Fabricante", "EstadoID", "dbo.Estado", "EstadoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fabricante", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Fabricante", "CidadeID", "dbo.Cidade");
            DropIndex("dbo.Fabricante", new[] { "CidadeID" });
            DropIndex("dbo.Fabricante", new[] { "EstadoID" });
            DropColumn("dbo.Fabricante", "CidadeID");
            DropColumn("dbo.Fabricante", "EstadoID");
        }
    }
}
