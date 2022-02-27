using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace proj_ECommerce.DAL
{
    public class MySQLPersistence
    {
        int _ultimoId;
        bool _manterConexaoAberta = false;

        MySqlConnection _conexao { get; set; }
        MySqlCommand _cmd { get; set; }
        MySqlTransaction _transacao { get; set; }


        public int UltimoId { get => _ultimoId; set => _ultimoId = value; }

        /* Cria uma conexão com o Banco, e uma variável de comandos SQL. */
        public MySQLPersistence(bool manter = false)
        {
            _conexao = new MySqlConnection("CONNECTION STRING");
            _cmd = _conexao.CreateCommand();
            _manterConexaoAberta = manter;
        }

        /* Abre a conexão com o Banco. */
        public void Abrir()
        {
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                _conexao.Open();
            }
        }


        /* Fecha a conexão com o Banco. */
        public void Fechar()
        {
            _conexao.Close();
        }


        /* Inicia uma Transação com o Banco. */
        public void IniciarTransacao()
        {
            Abrir();
          
            _transacao = _conexao.BeginTransaction();
            _cmd.Transaction = _transacao;
        }


        /* Efetua um Commit caso a transação suceda. */
        public void TransacaoCommit()
        {
            if (_transacao != null)
            {
                _transacao.Commit();
                _transacao.Dispose();
                _transacao = null;
            }
        }


        /* Efetua um RollBack caso a transação falhe. */
        public void TransacaoRollback()
        {
            if (_transacao != null)
            {
                _transacao.Rollback();
                _transacao.Dispose();
                _transacao = null;
            }
        }


        /* Efetua uma operação INSERT, UPDATE ou DELETE com o banco, recebendo o SQL e seus parâmetros.*/
        public int ExecutarNonQuery(string SQL, Dictionary<string, object> parametros = null)
        {
            int rowCount;

            Abrir();
            _cmd.CommandText = SQL;
           
            /*Percorre os params inserindo-os no _cmd.*/
            if(parametros != null)
            {
                foreach(var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

  
            /*Obtém a quantidade de linhas afetadas pela operação..*/
            rowCount = _cmd.ExecuteNonQuery();

            /*Obtém o último ID inserido..*/
            _ultimoId = (int) _cmd.LastInsertedId;

            if (!_manterConexaoAberta)
                Fechar();

            return rowCount;
        }


        /* Efetua um SelectScalar no Banco, recebe o SQL e os parâmetros. */
        public object ExecutarSelectScalar(string SQL, Dictionary<string, object> parametros = null)
        {
            Abrir();
            _cmd.CommandText = SQL;

            /*Percorre os params inserindo-os no _cmd.*/
            if (parametros != null)
            {
                foreach(var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            object res = _cmd.ExecuteScalar();
            
            if (!_manterConexaoAberta)
                Fechar();

            return res;
        }


        /* Efetua um Select SQL no banco. Recebe o SQL e os parametros. */
        public DbDataReader ExecutarSelect(string SQL, Dictionary<string, object> parametros = null)
        {
            Abrir();
            _cmd.CommandText = SQL;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            MySqlDataReader reader = _cmd.ExecuteReader();

            return reader;
        }


        /* Limpa os parâmetros do objeto cmd. */
        public void LimparParametros()
        {
            _cmd.Parameters.Clear();
        }
    }
}
