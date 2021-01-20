using proj_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.DAL
{
    public class UsuarioDAO
    {
        MySQLPersistence _conexao = new MySQLPersistence();

        /* Tenta obter um usuário a partir do seu ID. */
        public Usuario Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM usuario WHERE usu_id = @id";
            Usuario result;

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

        /* Busca um usuario no banco.. */
        public List<Usuario> Buscar(string nome)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string SQL = "SELECT * FROM usuario WHERE usu_nome LIKE @Nome";

            parametros.Add("@Nome", $"%{nome}%");

            /*Obtendo o resultado da Query..*/
            DbDataReader reader = _conexao.ExecutarSelect(SQL, parametros);

            /*Mapeando em uma lista e fechando a conexão que foi aberta no ExecutarSelect()*/
            List<Usuario> lista = Map(reader);
            _conexao.Fechar();

            return lista;
        }


        /* Faz o mapeando de um DbDR em uma List<Usuario>. Recebe o DR, retorna  Lista.*/
        private List<Usuario> Map(DbDataReader dr)
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario u; 

            /*Percorre o DbDR gerando a Lista..*/
            while (dr.Read())
            {
                u = new Models.Usuario();
                
                /*Criando o Obj. Usuario..*/
                u.Id = Convert.ToInt32(dr["usu_id"]);
                u.Nome = dr["usu_nome"].ToString();
                u.Senha = dr["usu_senha"].ToString();
                u.Email = dr["usu_email"].ToString();

                lista.Add(u);
            }

            return lista;
        }


        /* Grava o Usuário no Banco.*/
        public bool Gravar(Usuario u)
        {
            string SQL = "INSERT INTO usuario(usu_nome, usu_email, usu_senha) VALUES(@Nome, @Email, @Senha)";
            MySQLPersistence bd = new MySQLPersistence(true);
            Dictionary<string, object> parametros;
            int rowCount = 0;

            try
            {
                /* Inicia uma Transação.*/
                bd.IniciarTransacao();

                /* Adicionando parametros.. */
                parametros = new Dictionary<string, object>();
                parametros.Add("@Nome", u.Nome);
                parametros.Add("@Email", u.Email);
                parametros.Add("@Senha", u.Senha);

                /* Executando o INSERT USUARIO.. */
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

        /*Verifica se um usuário existe no banco. Usado para autenticação.*/
        public bool Exists(string email, string senha)
        {
            string SQL = "SELECT COUNT(*) FROM usuario WHERE usu_email = @Email and usu_senha = @Senha";
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@Email", email);
            parametros.Add("@Senha", senha);

            /*Verifica se o retorno é válido ou não..*/
            object ret = _conexao.ExecutarSelectScalar(SQL, parametros);
            if (ret == null || Convert.ToInt32(ret) == 0)
                return false;

            return true;
        }
    }
}
