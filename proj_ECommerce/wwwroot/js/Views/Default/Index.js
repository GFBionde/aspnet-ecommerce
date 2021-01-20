/* Script com as funcionalidades do ../Default/Index! */
let index = {

    listarGrid: function () {
        // Requisição com o servidor..
        HTTPClient.post("Default/ListarProdutos")
            .then(function (retornoServidor) {
                return retornoServidor.json();
            })
            // Tenta carregar a lista de Produtos..
            .then(function (obj) {
                index.carregarGrid(obj);
            })
            // Falhou.
            .catch(function () {
                toastr.error('Erro ao carregar Produtos. ');
            });
    },


    // Carrega a grid, apos receber a lista vindo do servidor.
    carregarGrid: function (lista) {
        let listResultado = document.getElementById('box-produtos');
        let linhas = "";
        var count = 0;

        // deus perdoe minhas gambiarras com javascript
        console.log('NO ONE EXPECTS THE SPANISH INQUISITION!!!');

        if (lista != null)
            linhas += `<div class="row">`;


        lista.forEach((item) => {
            if (count != 0 && count % 3 == 0) {
                linhas += `</div>`;

                if (count < lista.length)
                    linhas += `<br/><div class="row">`;
            }


            linhas += `
            <div class="col">
                <div class="block-4 text-center">
                    <!-- Foto do Produto -->
                    <figure class="block-4-image">
                        <img id="prodImagem" src="${item.urlImagem}" alt="Indisponivel" class="img-fluid" style="height: 220px;">
                    </figure>

                    <!-- Legenda da foto -->
                    <div class="block-4-text p-4">
                        <!-- Nome do Produto -->
                        <h3 id="prodNome"> ${item.nome} </h3>
                            
                        <!-- Preço de Venda -->
                        <p id="prodPreco" class="text-primary font-weight-bold"> R$ ${item.pvenda} </p>
                            
                        <!-- Add no Carrinho -->
                        <p>
                            <a id="addCarrinho" href="javascript:void(0);" class="mb-0 text-center"
                                onclick="index.addCarrinho(${item.id}, '${item.nome}', ${item.pvenda}, '${item.urlImagem}')"> 
                                    + Adicionar ao Carrinho 
                            </a>
                        </p>
                    </div>
                </div>
            </div>`;

            count++;
        });

        listResultado.innerHTML += linhas;
    },


    // Adiciona o produto no carrinho
    addCarrinho: function (id, nome, pvenda, urlImagem) {
        var carrinho = [];
        var dt = Date.now();
        if (sessionStorage.getItem("carrinho") != null)
            carrinho = JSON.parse(sessionStorage.getItem("carrinho"));

        // Inserindo o item no carrinho
        carrinho.push({
            id: id,
            nome: nome,
            pvenda: pvenda,
            quantidade: 1,
            total: pvenda,
            urlImagem: urlImagem,
            uid: dt,
        });

        // Salvando na sessionStorage
        sessionStorage.setItem("carrinho", JSON.stringify(carrinho));

        // Feedback para o usuário..
        toastr.success('Adicionado ao carrinho!');
    }
}