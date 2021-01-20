using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.Models
{
    public class Carrinho
    {
        List<ItemCarrinho> _items = new List<ItemCarrinho>();
        double _total;
        double _desconto;

        public Carrinho()
        {
            this.Total = 0;
            this.Desconto = 0;
        }


        public Carrinho(List<ItemCarrinho> items, double total, double desconto)
        {
            _items = items;
            _total = total;
            _desconto = desconto;
        }


        // Getters and Setters
        public List<ItemCarrinho> Items
        {
            get => _items;
            set
            {
                if (value == null)
                    throw new Exception("Itens inválidos");

                _items = value;
            }
        }


        public double Total
        {
            get => _total;
            set
            {
                if (value < 0)
                    throw new Exception("Total inválido.");

                _total = value;
            }
        }


        public double Desconto
        {
            get => _desconto;
            set
            {
                if (value < 0)
                    throw new Exception("Desconto inválido.");

                _total = value;
            }
        }


        // Aplica desconto ao total do carrinho.
        public void AplicarDesconto()
        {
            this.Total -= this.Desconto;
        }


        // Calcula o valor total do carrinho.
        public void calcularTotal()
        {
            this.Total = 0; 
            
            foreach(ItemCarrinho item in Items)
            {
                this.Total += item.calcularTotal();
            }
        }


        // Adiciona um item no carrinho.
        public void adicionarItem(ItemCarrinho item)
        {
            this.Items.Add(item);
        }
        
        
        // Remove um item do carrinho
        public void removerItem(ItemCarrinho item)
        {
            this.Items.Remove(item);
        }


        // Grava o carrinho no banco de dados..
        public void concluirCompra()
        {
            // To do!
        }



    }
}
