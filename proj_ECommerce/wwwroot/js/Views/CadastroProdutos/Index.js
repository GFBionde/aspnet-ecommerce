/* Script com as funcionalidades do ../CadastroProdutos/Index!*/
let index = {

    /* Efetua a validação dos inputs e responde com o cadastro do produto, ou aviso de dados inválidos.*/
    cadastrar: function () {
        let id = document.querySelector("#inpID");
        let nome = document.querySelector("#inpNome");
        let categ = document.querySelector("#inpCategoria");
        let pcompra = document.querySelector("#inpPCompra");
        let pvenda = document.querySelector("#inpPVenda");
        let urlimagem = document.querySelector("#inpUrlImagem");

        let cat_id = document.getElementById("idCategoria");
        let cat_nome = document.getElementById("inpCategoria");

        var divMsg = document.getElementById("divMsg");
        var error = false;

        /* Verifica se os inputs estão vazios.*/
        if (id.value.trim() == "" || nome.value.trim() == "") 
            error = true;

        if (categ.value.trim() == "") 
            error = true;

        if (pcompra.value.trim() == "" || pvenda.value.trim() == "")
            error = true;

        if (cat_id.value.trim() == "" || cat_nome.value.trim() == "")
            error = true;

        /* Processa e devolve o retorno. */
        if (error) {
            divMsg.innerHTML = "Preencha todos os campos para prosseguir.";
            divMsg.style.display = "block";
        }
        else {
            /* Efetua o AJAX. */
            let dadosProduto = {
                id: id.value,
                nome: nome.value,
                pcompra: pcompra.value,
                pvenda: pvenda.value,
                catid: cat_id.value,
                catnome: cat_nome.value,
                urlimagem: urlimagem.value,
            };

            /*Chama a classe de requisição, recebe a Promise como retorno. */
            HTTPClient.post("Cadastrar", dadosProduto)

                /*JSON vindo do servidor -> obj. literal*/
                .then(function (retornoServidor) {
                    return retornoServidor.json();
                })

                /* Resultado == Sucesso. */
                .then(function (obj) {
                    if (obj.operacao) {
                        /*Mensagem de sucesso..*/
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
    },


    /*Faz a requisição para obter os dados da lista, e faz o carregamento no html. */
    listarCategorias: function () {
        let loading = document.getElementById('gifLoading');

        // Imagem de loading..
        loading.src = '../../loading.gif';
        loading.style.width = '35px';

        // Requisição com o servidor..
        HTTPClient.post("ObterCategorias")
            .then(function (retornoServidor) {
                return retornoServidor.json();
            })
            // Tenta carregar a lista de categorias..
            .then(function (obj) {
                index.carregarLista(obj);
            })
            // Falhou.
            .catch(function () {
                toastr.error('Erro ao carregar informações de Categoria. ');
            });

        // Invalidando o gif de loading..
        loading.src = '';
        loading.style.display = "none";
    },


    /* Carrega os dados  */
    carregarLista: function (lista) {
        let listResultado = document.getElementById('listCategoria');
        let linhas = "";

        // Criando o resultado HTML..
        lista.forEach((item) => {
            linhas +=
                `<li class="list-group-item list-group-item-action" >
                    <a href="javascript:void(0);" onclick="index.confirmarCategoria(${item.id}, '${item.nome}')">
                        ${item.nome}
                    </a>
                </li>`;
        });

        // Transferindo o HTML criado acima para o doc..
        listResultado.innerHTML = linhas;
    },


    /* Insere o elemento dentro do input disabled.*/
    confirmarCategoria: function (id, nome) {
        let inpCategoria = document.getElementById('inpCategoria');
        let idCategoria = document.getElementById('idCategoria');

        // Transferindo a categoria selecionada para o input..
        inpCategoria.value = nome;
        idCategoria.value = id;

        // Feedback para o usuário..
        toastr.success('Categoria: [Cód. ' + id + ' - ' + nome + '] selecionada!');
    },

}



