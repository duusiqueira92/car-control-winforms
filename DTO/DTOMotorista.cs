using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOMotorista
    {
        private int id_motorista;
        public int Id_Motorista
        {
            get { return id_motorista; }
            set { id_motorista = value; }
        }

        private int motorista_ativo;
        public int Motorista_Ativo
        {
            get { return motorista_ativo; }
            set { motorista_ativo = value; }
        }

        private int motorista_cadastro;
        public int Motorista_cadastro
        {
            get { return motorista_cadastro; }
            set { motorista_cadastro = value; }
        }

        private int id_funcionario;
        public int Id_Funcionario
        {
            get { return id_funcionario; }
            set { id_funcionario = value; }
        }

        private string cnh;
        public string Cnh
        {
            get { return cnh; }
            set { cnh = value; }
        }

        private string registro_cnh;
        public string Registro_Cnh
        {
            get { return registro_cnh; }
            set { registro_cnh = value; }
        }

        private int categoria;
        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        private string validade;
        public string Validade
        {
            get { return validade; }
            set { validade = value; }
        }

        private string cadastro;
        public string Cadastro
        {
            get { return cadastro; }
            set { cadastro = value; }
        }

    }
}
