let index = {

    // Obtem o carrinho da sessionStorage, e o percorre gerando HTML.
    carregarCarrinho: function () {
        var listResult = document.getElementById('body-carrinho');
        var tbHead = document.getElementById('head-carrinho');
        
        // Tenta obter o carrinho..
        if (sessionStorage.getItem("carrinho") != null) {
            var carrinho = JSON.parse(sessionStorage.getItem("carrinho"));
            tbHead.style.display = 'contents';

            var linhas = '';
            // Criando O HTML das linhas de produtos do carrinho..
            carrinho.forEach((item) => {
                linhas += `
                <tr>
                    <!-- Foto do produto -->
                    <td class="product-thumbnail">
                        <img id="prodImagem" alt="Not found" class="img-fluid" src="${item.urlImagem}" style="height: 110px;">
                    </td>

                    <!-- Nome do produto -->
                    <td class="product-name">
                        <h2 id="prodNome" class="h5 text-black">${item.nome} </h2>
                    </td>

                    <!-- Preço do produto -->
                    <td id="prodPreco">R$ ${item.pvenda}</td>


                    <!-- Valor total = Preço do Produto * Quantidade -->
                    <td id="prodValorTotal">R$ ${item.pvenda}</td>

                    <!-- Remover produto do Carrinho -->
                    <td>
                        <a href="javascript:void(0)" class="btn btn-primary btn-sm" onclick="index.remover(${item.uid})"> X </a>
                    </td>
                </tr>
            `;
            });

            // Adicionando as linhas na tabela!
            listResult.innerHTML += linhas;
        }
        else {
            tbHead.style.display = 'none';
            listResult.innerHTML += `<tr> <td colspan="5">  <h4> Seu carrinho está vazio. </h4> </td> </tr>`;
        }
             
    },


    // Remove um item do carrinho, dado o uid dele.
    remover: function (uid) {
        var carrinho = [];

        if (sessionStorage.getItem("carrinho") != null) {
            carrinho = JSON.parse(sessionStorage.getItem("carrinho"));
            
            // Busca exaustiva procurando o item no vetor..
            var pos = 0;
            while (pos < carrinho.length && carrinho[pos].uid != uid) { pos++ };

            // Removendo o item.
            if(pos < carrinho.length)
                carrinho.splice(pos, 1);

            // Atualiza o carrinho
            sessionStorage.setItem("carrinho", JSON.stringify(carrinho));

            if (carrinho.length == 0)
                sessionStorage.removeItem("carrinho");
            window.location.href = "/Carrinho";
        }

        console.log('Removido do Carrinho!');
    },


    // Percorre os itens do carrinho calculando o total.
    calcularTotal: function () {
        var carrinho = [];
        var total = 0.0;

        if (sessionStorage.getItem("carrinho") != null) {
            carrinho = JSON.parse(sessionStorage.getItem("carrinho"));

            carrinho.forEach((item) => {
                total += item.pvenda;
            });
        }

        var cart_subTotal = document.getElementById('cart_subTotal');
        var cart_total = document.getElementById('cart_total');

        cart_subTotal.innerHTML = 'R$ ' + total;
        cart_total.innerHTML = 'R$ ' + total;
    }
}