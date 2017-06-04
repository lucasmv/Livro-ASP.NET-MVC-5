namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicicaoCidadeEstado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeID = c.Long(nullable: false, identity: true),
                        EstadoID = c.Long(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.CidadeID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoID = c.Long(nullable: false, identity: true),
                        UF = c.String(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EstadoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cidade", "EstadoID", "dbo.Estado");
            DropIndex("dbo.Cidade", new[] { "EstadoID" });
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
