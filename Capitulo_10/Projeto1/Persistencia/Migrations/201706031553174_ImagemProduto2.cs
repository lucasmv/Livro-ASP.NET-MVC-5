namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagemProduto2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "NomeArquivo", c => c.String());
            AddColumn("dbo.Produto", "TamanhoArquivo", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "TamanhoArquivo");
            DropColumn("dbo.Produto", "NomeArquivo");
        }
    }
}
