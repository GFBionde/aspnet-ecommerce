/* Script com as funcionalidades do ../Login/Index!*/
let index = {

    /* Efetua a validação dos inputs e responde com o login ou aviso de dados inválidos.*/
    logar: function () {
        let email = document.querySelector("#inpEmail");
        let senha = document.querySelector("#inpSenha");

        var divMsg = document.getElementById("divMsg");

        if (email.value.trim() == "" || senha.value.trim() == "") {
            divMsg.innerHTML = "Preencha todos os campos para prosseguir.";
            divMsg.style.display = "block";
        }
        else {
            /* Efetua o AJAX. */
            let dadosUsuario = {
                email: email.value, 
                senha: senha.value
            };

            /*Chama a classe de requisição, recebe a Promise como retorno. */
            HTTPClient.post("/Login/Logar", dadosUsuario)
            /*JSON vindo do servidor -> obj. literal*/
            .then(function (retornoServidor) {
                return retornoServidor.json(); 
            })

            /* Resultado == Sucesso. */
            .then(function (obj) {
                if (obj.operacao) {
                    window.location.href = "CadastroProdutos/Index"; /*Redirecionar para Controller/Action */
                    toastr.success(obj.msg);
                }
                else {
                    toastr.error(obj.msg);
                }
            })

            /* Resultado == Erro. */
            .catch(function () {
                toastr.error('Erro ao processar as informações. Tente novamente mais tarde.');
            })
        }
    }
}



