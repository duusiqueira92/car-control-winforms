using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOUsuario
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nome_usuario;
        public string Nome_usuario
        {
            get { return nome_usuario; }
            set { nome_usuario = value; }
        }

        private string data_cadstro;
        public string Data_cadastro
        {
            get { return data_cadstro; }
            set { data_cadstro = value; }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        private int login_cadastrado;
        public int Login_cadastrado
        {
            get { return login_cadastrado; }
            set { login_cadastrado = value; }
        }

        private int id_usuario;
        public int Id_usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }
    }
}
