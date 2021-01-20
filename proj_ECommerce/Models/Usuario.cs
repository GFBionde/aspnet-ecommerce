using proj_ECommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
/*
    Cases recomendadas para C#
    > Nome de Atributo:    _nome
    > Nome de Variável:    nome
    > Nome de Parâmetro:   nome
    > Nome de Método:      GravarCliente()
    > Nome de Classe:      NomeDaClasse 

    OBS: Em C#, usamos propriedades get/set (e não métodos), portanto, a sintaxe é diferente.
 */

namespace proj_ECommerce.Models
{
    public class Usuario
    {
        int _id;
        string _nome;
        string _email;
        string _senha;

        /* ### Builders. ### */
        public Usuario()
        {

        }

        /* ### Getters and Setters. ### */
        public string Nome
        {
            get => _nome;
            set
            {
                if (value == "" || value == null)
                    throw new Exception("Nome não pode estar vazio!");

                if (!value.All(char.IsLetter))
                    throw new Exception("Nome deve conter somente números!");

                _nome = value;
            }
        }

        public int Id 
        { 
            get => _id; 
            set
            {
                if (!value.ToString().All(char.IsDigit) || value <= 0)
                    throw new Exception("ID deve ser composto por numeros maiores que 0.");
                
                _id = value;
            }
        }

        public string Email { 
            get => _email;
            set
            {
                if(value == "" || value == null)
                    throw new Exception("E-mail não pode estar vazio!");
               
                _email = value;
            }
        }
        public string Senha 
        { 
            get => _senha; 
            set
            {
                if (value == "" || value == null)
                    throw new Exception("Senha não pode estar vazia!");
                
                _senha = value;
            }
        }

        /* ### Methods. ### */
        
        /* Efetua a validação do e-mail e senha enviados por parâmetro. Retorna true caso os dados sejam válidos. */
        public bool ValidarSenha(string email, string senha)
        {
            if (!email.Contains("@") || !email.Contains("."))
                return false;

            if (senha.Length < 6)
                return false;

            return true;
        }

        /* ### DAO Connection. ###*/

        /* Verifica se um usuário com o dado Email e Senha existe no banco.*/
        public bool Exists(string email, string senha)
        {
            DAL.UsuarioDAO uDAO = new DAL.UsuarioDAO();

            return uDAO.Exists(email, senha);
        }
        
        /* Tenta obter um usuario, dado o ID dele.*/
        public bool Obter(int id)
        {
            DAL.UsuarioDAO uDAO = new DAL.UsuarioDAO();
            Usuario res = uDAO.Obter(id);

            if (res == null)
                return false;

            /* Setando novos valores.. */
            this.Id = id;
            this.Nome = res.Nome;
            this.Email = res.Email;
            this.Senha = res.Senha;

            return true;
        }

        /* Efetua a gravação do Obj. na respectiva tabela do banco de dados.*/
        public bool Gravar()
        {
            DAL.UsuarioDAO uDAO = new DAL.UsuarioDAO();

            return uDAO.Gravar(this);
        }

    }
}
