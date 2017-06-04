using Servico.Tabelas;
using System.Web.Mvc;

namespace Projeto1.Areas.Tabelas.Controllers
{
    public class CidadesController : Controller
    {
        private CidadeServico cidadeServico = new CidadeServico();

        public JsonResult GetCidadesDoEstado(int estadoID)
        {
            return Json(cidadeServico.ObterCidadesPorEstado(estadoID), JsonRequestBehavior.AllowGet);
        }
    }
}