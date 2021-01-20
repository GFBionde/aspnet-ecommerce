using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.Models
{
    public class Categoria
    {
        int _id;
        string _nome;

        /* ### Builders. ### */
        public Categoria(){}

        public Categoria(int id, string nome)
        {
            _id = id;
            _nome = nome;
        }

        /* ### Getters and Setters. ### */
        public int Id { 
            get => _id;
            set
            {
                if (value <= 0)
                    throw new Exception("Categoria ID deve ser um numero maior que 0.");

                _id = value;
            }
        }


        public string Nome { 
            get => _nome;
            set
            {
                if (value == "" || value == null)
                    throw new Exception("Nome da Categoria não pode estar vazio!");

                _nome = value;
            }
        }


        /* ### Methods. ### */
        /* Verifica se uma Categoria com o @nome existe no banco.*/
        public List<Categoria> Buscar(string nome)
        {
            DAL.CategoriaDAO cDAO = new DAL.CategoriaDAO();

            return cDAO.Buscar(nome);
        }


        /* Tenta obter uma Categoria, dado o @ID dela.*/
        public bool Obter(int id)
        {
            DAL.CategoriaDAO cDAO = new DAL.CategoriaDAO();
            Categoria res = cDAO.Obter(id);

            if (res == null)
                return false;

            /* Setando novos valores.. */
            this.Id = id;
            this.Nome = res.Nome;

            return true;
        }
    }
}
