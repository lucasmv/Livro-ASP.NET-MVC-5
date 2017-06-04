using Microsoft.AspNet.Identity.EntityFramework;
using Projeto1.Areas.Seguranca.Models;
using System.Data.Entity;

namespace Projeto1.DAL
{
    public class IdentityDbContextAplicacao : IdentityDbContext<Usuario>
    {
        public IdentityDbContextAplicacao() : base("Asp_Net_MVC_CS")
        {
        }

        static IdentityDbContextAplicacao()
        {
            Database.SetInitializer(new IdentityDbInit());
        }

        public static IdentityDbContextAplicacao Create()
        {
            return new IdentityDbContextAplicacao();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<IdentityDbContextAplicacao>
    {
    }
}