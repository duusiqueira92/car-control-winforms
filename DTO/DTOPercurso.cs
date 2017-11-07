using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPercurso
    {
        private int id_trajeto;
        public int Id_trajeto
        {
            get { return id_trajeto; }
            set { id_trajeto = value; }
        }

        private int ativo;
        public int Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private int veiculo_ativo;
        public int Veiculo_Ativo
        {
            get { return veiculo_ativo; }
            set { veiculo_ativo = value; }
        }

        private int motorista_ativo;
        public int Motorista_Ativo
        {
            get { return motorista_ativo; }
            set { motorista_ativo = value; }
        }

        private int id_veiculo;
        public int Id_veiculo
        {
            get { return id_veiculo; }
            set { id_veiculo = value; }
        }

        private string abastecido;
        public string Abastecido
        {
            get { return abastecido; }
            set { abastecido = value; }
        }

        private string periodo_inicial;
        public string Periodo_inicial
        {
            get { return periodo_inicial; }
            set { periodo_inicial = value; }
        }

        private string periodo_final;
        public string Periodo_final
        {
            get { return periodo_final; }
            set { periodo_final = value; }
        }

        private int km_veiculo;
        public int Km_veiculo
        {
            get { return km_veiculo; }
            set { km_veiculo = value; }
        }

        private int combustivel_abastecimento;
        public int Combustivel_abastecimento
        {
            get { return combustivel_abastecimento; }
            set { combustivel_abastecimento = value; }
        }

        private int motorista;
        public int Motorista
        {
            get { return motorista; }
            set { motorista = value; }
        }

        private decimal litros_abastecido;
        public decimal Litros_abastecido
        {
            get { return litros_abastecido; }
            set { litros_abastecido = value; }
        }

        private int veiculo;
        public int Veiculo
        {
            get { return veiculo; }
            set { veiculo = value; }
        }

        private string hora_inicial;
        public string Hora_inicial
        {
            get { return hora_inicial; }
            set { hora_inicial = value; }
        }

        private string hora_final;
        public string Hora_final
        {
            get { return hora_final; }
            set { hora_final = value; }
        }
    }
}
