using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.Models
{
    public class ItemCarrinho
    {
        Produto _produto;
        int _quantidade;
        double _total;


        public ItemCarrinho() { }


        public ItemCarrinho(Produto produto, int quantidade, double total)
        {
            _produto = produto;
            _quantidade = quantidade;
            _total = total;
        }


        // Getters and Setters.
        public Produto Produto
        {
            get => _produto;
            set
            {
                if (value == null)
                    throw new Exception("Selecione um produto válido.");

                _produto = value;
            }
        }


        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                    throw new Exception("Quantidade inválida.");

                _quantidade = value;
            }
        }


        public double Total
        {
            get => _total;
            set
            {
                if (value < 0)
                    throw new Exception("Valor total inválido.");

                _total = value;
            }
        }


        // Calcula e retorna o valor total deste item (quantidade * preço)
        public double calcularTotal()
        {
            return this.Quantidade * this.Produto.Pvenda;
        }



        // Diminui a quantidade do produto..
        public void diminuirQuantidade(int qtd)
        {
            this.Quantidade -= qtd;
        } 


        // Aumenta a quantidade do produto..
        public void aumentarQuantidade(int qtd)
        {
            this.Quantidade += qtd;
        }

    }
}
