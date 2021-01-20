using proj_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.DAL
{
    public class CategoriaDAO
    {
        MySQLPersistence _conexao = new MySQLPersistence();

        /* Tenta obter uma categoria a partir do seu ID. */
        public Categoria Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM categoria WHERE cat_id = @id";
            Categoria result;

            /* Executa o SELECT..*/
            parametros.Add("@id", id);
            DbDataReader reader = _conexao.ExecutarSelect(SQL, parametros);

            /* ..Verifica o resultado */
            if (reader.HasRows)
            {
                result = Map(reader).First();
            }
            else
            {
                result = null;
            }

            _conexao.Fechar();

            return result;
        }


        /* Busca um Categoria no banco.. */
        public List<Categoria> Buscar(string nome)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM categoria WHERE cat_nome LIKE @Nome";


            /*Obtendo o resultado da Query..*/
            parametros.Add("@Nome", $"%{nome}%");
            DbDataReader reader = _conexao.ExecutarSelect(SQL, parametros);

            /*Mapeando em uma lista e fechando a conexão que foi aberta no ExecutarSelect()*/
            List<Categoria> lista = Map(reader);
            _conexao.Fechar();

            return lista;
        }


        /* Faz o mapeando de um DbDR em uma List<Categoria>. Recebe o DR, retorna  Lista.*/
        private List<Categoria> Map(DbDataReader dr)
        {
            List<Categoria> lista = new List<Categoria>();
            Categoria c;

            /*Percorre o DbDR gerando a Lista..*/
            while (dr.Read())
            {
                /*Criando o Obj. Categoria..*/
                c = new Categoria
                {
                    Id = Convert.ToInt32(dr["cat_id"]),
                    Nome = dr["cat_nome"].ToString()
                };

                lista.Add(c);
            }

            return lista;
        }
    }
}
