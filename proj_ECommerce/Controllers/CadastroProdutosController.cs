using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proj_ECommerce.Models;

namespace proj_ECommerce.Controllers
{
    [Authorize("CookieAuth")]
    public class CadastroProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        /* Faz o cadastro do Produto. Recebe JSON do produto + categoria.*/
        public IActionResult Cadastrar([FromBody] System.Text.Json.JsonElement dados)
        {
            bool operacao = false;
            string msg = "";

            /* Validando as variaveis..*/
            if (!int.TryParse(dados.GetProperty("id").ToString(), out int id))
                msg = "Erro ao processar o ID.";

            if(!double.TryParse(dados.GetProperty("pcompra").ToString(), out double pc))
                msg = "Erro ao processar o Preço de Compra.";

            if (!double.TryParse(dados.GetProperty("pvenda").ToString(), out double pv))
                msg = "Erro ao processar o Preço de Venda.";

            if (!int.TryParse(dados.GetProperty("catid").ToString(), out int catid))
                msg = "Erro ao processar o ID da Categoria.";

            /* Tentando criar o objeto produto. */
            if (msg == "")
            {
                try
                {
                    Models.Categoria categ = new Models.Categoria(catid, dados.GetProperty("catnome").ToString());
                    Models.Produto p = new Models.Produto(id, dados.GetProperty("nome").ToString(), 
                        pc, pv, categ, dados.GetProperty("urlimagem").ToString());

                    if (p.Gravar())
                    {
                        operacao = true;
                        msg = "Produto cadastrado!";
                    }
                    else
                    {
                        msg = "Erro ao cadastrar Produto.";
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.ToString();
                }
            }

            /* Cria um Obj. anônimo (igual ao Obj. Literal do JS). */
            var retorno = new
            {
                operacao,
                msg
            };

            /* Retorna um JSON. */
            return Json(retorno);
        }


        
        
        /* Obtém a lista de 'Categorias' através do Banco de Dados.*/
        public IActionResult ObterCategorias()
        {
            Models.Categoria c = new Categoria();
            return Json(c.Buscar(""));
        }
    }
}
