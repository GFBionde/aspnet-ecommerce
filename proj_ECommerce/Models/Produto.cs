using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.Models
{
    public class Produto
    {
        int _id;
        string _nome;
        double _pcompra;
        double _pvenda;
        string _urlImagem;
        Categoria _categ;


        public Produto() {
            this.Categ = new Categoria();
        }

        public Produto(int id, string nome, double pcompra, double pvenda, Categoria categ, String urlImagem)
        {
            this.Categ = new Categoria();

            _id = id;
            _nome = nome;
            _pcompra = pcompra;
            _pvenda = pvenda;
            _categ = categ;
            _urlImagem = urlImagem;
        }


        // Getters and Setters
        public int Id 
        { 
            get => _id; 
            set
            {
                if (value <= 0)
                    throw new Exception("ID deve ser um numero maior que 0.");

                _id = value;
            }
        }

        public string Nome 
        { 
            get => _nome;
            set 
            {
                if (value == "" || value == null)
                    throw new Exception("Nome não pode estar vazio!");
                
                _nome = value;
            }  
        }

        public Categoria Categ 
        { 
            get => _categ; 
            set
            {
                if (value == null || value.Nome == "")
                    throw new Exception("Categoria não pode estar vazia!");

                _categ = value;
            }
        }

        public double Pcompra 
        { 
            get => _pcompra;
            set
            {
                if (value < 0)
                    throw new Exception("Preço de compra deve ser maior ou igual à 0!");

                _pcompra = value;
            }
        }

        public double Pvenda 
        { 
            get => _pvenda;
            set
            {
                if (value < 0)
                    throw new Exception("Preço de venda deve ser maior ou igual à 0!");

                _pvenda = value;
            }
        }


        public string UrlImagem
        {
            get => _urlImagem;
            set
            {
                _urlImagem = value;
            }
        }



        /* ### DAO Connection. ###*/

        /* Tenta obter um Produto, dado o ID dele.*/
        public bool Obter(int id)
        {
            DAL.ProdutoDAO pDAO = new DAL.ProdutoDAO();
            Produto res = pDAO.Obter(id);

            if (res == null)
                return false;

            /* Setando novos valores.. */
            this.Id = id;
            this.Nome = res.Nome;
            this.Pcompra = res.Pcompra;
            this.Pvenda = res.Pvenda;
            this.Categ.Id = res.Categ.Id;
            this.UrlImagem = res.UrlImagem;

            return true;
        }

        /* Efetua a gravação do Obj. na respectiva tabela do banco de dados.*/
        public bool Gravar()
        {
            DAL.ProdutoDAO pDAO = new DAL.ProdutoDAO();

            return pDAO.Gravar(this);
        }

        /* Efetua a operação de Busca na repectiva tabela do banco de dados.*/
        public List<Produto> Buscar(string nome)
        {
            DAL.ProdutoDAO pDAO = new DAL.ProdutoDAO();

            return pDAO.Buscar(nome);
        }

    }
}
