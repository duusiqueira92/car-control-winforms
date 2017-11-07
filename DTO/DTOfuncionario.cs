using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOfuncionario
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int motorista_cadastrado;
        public int Motorista_cadastrado
        {
            get { return motorista_cadastrado; }
            set { motorista_cadastrado = value; }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private string endereco;
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        private string bairro;
        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        private string cidade;
        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        private string numero;
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private string cep;
        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        private int cargo;
        public int Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        private string cpf;
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private string rg;
        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        private string telefone;
        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        private string celular;
        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }

        private string dataCadastro;
        public string DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }

        private string dataNascimento;
        public string DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }
    }
}
