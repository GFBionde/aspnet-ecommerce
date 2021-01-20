using proj_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.DAL
{
    public class ProdutoDAO
    {
        MySQLPersistence _conexao = new MySQLPersistence();

        /* Tenta obter um produto a partir do seu ID. */
        public Produto Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM produto WHERE prod_id = @id";
            Produto result;

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

        /* Busca um produto no banco.. */
        public List<Produto> Buscar(string nome)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM produto WHERE prod_nome LIKE @Nome";

            /*Obtendo o resultado da Query..*/
            parametros.Add("@Nome", $"%{nome}%");
            DbDataReader reader = _conexao.ExecutarSelect(SQL, parametros);

            /*Mapeando em uma lista e fechando a conexão que foi aberta no ExecutarSelect()*/
            List<Produto> lista = Map(reader);
            _conexao.Fechar();

            return lista;
        }


        /* Faz o mapeando de um DbDR em uma List<Produto>. Recebe o DR, retorna  Lista.*/
        private List<Produto> Map(DbDataReader dr)
        {
            List<Produto> lista = new List<Produto>();
            Produto p;

            /*Percorre o DbDR gerando a Lista..*/
            while (dr.Read())
            {
                /*Criando o Obj. Produto..*/
                p = new Produto();
                p.Categ = new Categoria();

                p.Id = Convert.ToInt32(dr["prod_id"]);
                p.Nome = dr["prod_nome"].ToString();
                p.Pcompra = Convert.ToDouble(dr["prod_pcompra"]);
                p.Pvenda = Convert.ToDouble(dr["prod_pvenda"]);
                p.UrlImagem = dr["prod_urlimagem"].ToString();

                p.Categ.Id = Convert.ToInt32(dr["fk_categoria"]);

                lista.Add(p);
            }

            return lista;
        }


        /* Grava o Produto no Banco.*/
        public bool Gravar(Produto p)
        {
            string SQL = "INSERT INTO Produto(prod_id, prod_nome, prod_pcompra, prod_pvenda, fk_categoria, prod_urlimagem) " +
                         "VALUES(@Id, @Nome, @Pcompra, @Pvenda, @Fk, @Urlimagem)";
            MySQLPersistence bd = new MySQLPersistence(true);
            Dictionary<string, object> parametros;
            int rowCount = 0;

            try
            {
                /* Inicia uma Transação.*/
                bd.IniciarTransacao();

                /* Adicionando parametros.. */
                parametros = new Dictionary<string, object>();
                parametros.Add("@Id", p.Id);
                parametros.Add("@Nome", p.Nome);
                parametros.Add("@Pcompra", p.Pcompra);
                parametros.Add("@Pvenda", p.Pvenda);
                parametros.Add("@Fk", p.Categ.Id);
                parametros.Add("@Urlimagem", p.UrlImagem);

                /* Executando o INSERT Produto.. */
                rowCount = bd.ExecutarNonQuery(SQL, parametros);

                /* Tenta executar o COMMIT (SUCESSO). */
                bd.TransacaoCommit();
            }
            catch
            {
                /* Executa o ROLLBACK (FALHA). */
                bd.TransacaoRollback();
            }

            bd.Fechar();
            return rowCount > 0;
        }
    }
}
