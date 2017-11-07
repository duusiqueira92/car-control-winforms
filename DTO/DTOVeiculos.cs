using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOVeiculos
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int ativo;
        public int Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        private string placa;
        public string Placa
        {
            get { return placa; }
            set { placa = value; }
        }

        private string ano;
        public string Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        private int tipo_combustivel;
        public int Tipo_combustivel
        {
            get { return tipo_combustivel; }
            set { tipo_combustivel = value; }
        }

        private int km_veiculo;
        public int Km_veiculo
        {
            get { return km_veiculo; }
            set { km_veiculo = value; }
        }

        private string data_cadastro;
        public string Data_cadastro
        {
            get { return data_cadastro; }
            set { data_cadastro = value; }
        }

    }
}
