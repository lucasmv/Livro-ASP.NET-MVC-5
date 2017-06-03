using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Projeto1.Areas.Seguranca.Models;
using Projeto1.DAL;
using System;

namespace Projeto1.Infraestrutura
{
    public class GerenciadorRole : RoleManager<Role>, IDisposable
    {
        public GerenciadorRole(RoleStore<Role> store) : base(store)
        {
        }

        public static GerenciadorRole Create(IdentityFactoryOptions<GerenciadorRole> options, IOwinContext context)
        {
            return new GerenciadorRole(new RoleStore<Role>(context.Get<IdentityDbContextAplicacao>()));
        }
    }
}