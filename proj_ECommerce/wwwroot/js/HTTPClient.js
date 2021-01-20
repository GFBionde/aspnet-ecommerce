/*
 * Biblioteca HTTPClient para realizar requisições POST e GET.
 * */

let HTTPClient = {

    /* Função anônima que envia o conteudo do parâmetro @dados para uma @url.*/
    post: function (url, dados) {

        let config = {
            method: "POST",
            body: JSON.stringify(dados),
            headers: {
                "Content-Type": "application/json"
            }
        }

        return fetch(url, config);
    },

    get: function (url) {

        let config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        }

        return fetch(url, config);
    }
}