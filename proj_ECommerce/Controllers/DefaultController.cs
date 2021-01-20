using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace proj_ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListarProdutos()
        {
            Models.Produto p = new Models.Produto();
            return Json(p.Buscar(""));
        }

    }
}
