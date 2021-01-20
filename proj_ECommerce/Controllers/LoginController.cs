using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace proj_ECommerce.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["CookieAuth"] != null)
            {
                return Redirect("CadastroProdutos/Index");
            }

            return View();
        }


        /* Efetua toda a operação de login, retorna um objeto JSON com o resultado do procedimento. */
        public IActionResult Logar([FromBody] System.Text.Json.JsonElement dados)
        {
            Models.Usuario user = new Models.Usuario();
            bool operacao = false;
            
            string msg;
            string email = dados.GetProperty("email").ToString();
            string senha = dados.GetProperty("senha").ToString();

            /* Chama o método de validação de e-mail e senha do usuário. */
            if (user.ValidarSenha(email, senha))
            {
                operacao = true;
                msg = "Bem-vindo!";


                #region Gerando a Cookie de Autorização
                var userClaims = new List<Claim>();
                
                userClaims.Add(new Claim("id", email));

                var identity = new ClaimsIdentity(userClaims, "Identificação do usuário");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                //Gerando a cookie.
                Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(HttpContext, principal);

                #endregion

            }
            else
            {
                msg = "E-mail ou senha inválidos.";
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


        [HttpGet]
        // Invalida os cookies de autenticação.
        public IActionResult Sair()
        {
            Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return Redirect("/Default/Index");
        }
    }
}
