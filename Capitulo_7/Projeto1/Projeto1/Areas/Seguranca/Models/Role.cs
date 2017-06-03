using Microsoft.AspNet.Identity.EntityFramework;

namespace Projeto1.Areas.Seguranca.Models
{
    public class Role : IdentityRole
    {
        public Role() : base() { }
        public Role(string name) : base(name) { }
    }
}