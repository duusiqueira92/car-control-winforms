using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class RelatorioAcessaDados
    {
        CriaBancoAcessoDados db;
        //carregar todos os motoristas cadastrados
        public DataTable SelecionaTodosMotoristas()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT id_motorista, nome, cnh, registro_cnh, validade_cnh, categoria FROM motorista m 
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario 
                                       INNER JOIN categoria c on  m.categoria_cnh = c.id_categoria WHERE motorista_ativo = 1 order by f.nome");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Motoristas: " + ex.Message, "Erro 0002");
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //carregar todos os veiculos cadastrados
        public DataTable SelecionaTodosVeiculos()
        {
            DataTable dt = new DataTable();
            try
            {
                db = new CriaBancoAcessoDados();
                db.Conectar();

                dt = db.RetDataTable(@"SELECT * FROM veiculo ORDER BY modelo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os veículos: " + ex.Message, "Erro 0002");
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para carregar o dgvTrajetos no evento Form_Load 
        public DataTable SelecionaTodosTrajetos()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT nome AS 'Nome', modelo AS 'Modelo', periodo_inicial AS 'Periodo Inicio', hora_inicio AS 'Hora Inicio',
                                       periodo_final as 'Periodo Termino', hora_fim AS 'Hora Termino' , 
                                       km_inicio AS 'KM Inicio', km_fim AS 'KM Fim', abastecido AS 'Abastecido?', 
                                       litros_abastecidos AS 'Litros Abastecidos' FROM dirige_trajeto dt
                                       INNER JOIN veiculo v ON dt.veiculo = v.id_veiculo
                                       INNER JOIN tipo_combustivel tc ON dt.combustivel_abastecimento = tc.id_combustivel
                                       INNER JOIN motorista m ON dt.motorista = m.id_motorista
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario ORDER BY nome");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Motoristas: " + ex.Message, "Erro 0002");
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para carregar uma tupla usando um id como parametro
        public DataTable SelecionaRelatorioIndividualMotorista(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                db = new CriaBancoAcessoDados();
                db.Conectar();

                dt = db.RetDataTable(@"SELECT nome AS 'Nome', count(id_trajeto) AS 'N° Viagens', sum(litros_abastecidos) AS 'Litros Abastecidos',
                                    (sum(km_fim) - sum(km_inicio)) AS 'KM Percorrido',
                                    avg(km_fim - km_inicio) AS 'Média/KM', avg(litros_abastecidos) AS 'KM/Litro' 
                                    FROM motorista m 
                                    INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario 
                                    INNER JOIN dirige_trajeto dt ON m.id_motorista = dt.motorista WHERE motorista = " + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar o Relatório de motoristas: " + ex.Message, "Erro 0002");
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para carregar uma tupla usando um id como parametro
        public DataTable SelecionaRelatorioIndividualVeiculo(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                db = new CriaBancoAcessoDados();
                db.Conectar();

                dt = db.RetDataTable(@"SELECT modelo AS 'Modelo', count(id_trajeto) AS 'N° Viagens', sum(litros_abastecidos) AS 'Litros Abastecidos',
                                    (sum(km_fim) - sum(km_inicio)) AS 'KM Percorrido',
                                    avg(km_fim - km_inicio) AS 'Média/KM', avg(litros_abastecidos) AS 'KM/Litro'
                                    FROM veiculo v 
                                    INNER JOIN dirige_trajeto dt ON v.id_veiculo = dt.veiculo WHERE veiculo  = " + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os veículos: " + ex.Message, "Erro 0002");
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisa relatorio de motorista
        public DataTable PesquisarRelatorioMotorista(string nome)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT nome AS 'Nome', modelo AS 'Modelo', periodo_inicial AS 'Periodo Inicio', hora_inicio AS 'Hora Inicio',
                                       periodo_final as 'Periodo Termino', hora_fim AS 'Hora Termino',
                                       km_inicio AS 'KM Inicio', km_fim AS 'KM Fim', abastecido AS 'Abastecido?',
                                       litros_abastecidos AS 'Litros Abastecidos' FROM dirige_trajeto dt
                                       INNER JOIN veiculo v ON dt.veiculo = v.id_veiculo
                                       INNER JOIN tipo_combustivel tc ON dt.combustivel_abastecimento = tc.id_combustivel
                                       INNER JOIN motorista m ON dt.motorista = m.id_motorista
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario WHERE nome LIKE '%" + nome + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar relatorio por NOME:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisa relatorio de veiculos
        public DataTable PesquisarRelatorioVeiculo(string modelo)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT modelo AS 'Modelo', nome AS 'Nome', periodo_inicial AS 'Periodo Inicio', hora_inicio AS 'Hora Inicio',
                                       periodo_final as 'Periodo Termino', hora_fim AS 'Hora Termino',
                                       km_inicio AS 'KM Inicio', km_fim AS 'KM Fim', abastecido AS 'Abastecido?',
                                       litros_abastecidos AS 'Litros Abastecidos' FROM dirige_trajeto dt
                                       INNER JOIN veiculo v ON dt.veiculo = v.id_veiculo
                                       INNER JOIN tipo_combustivel tc ON dt.combustivel_abastecimento = tc.id_combustivel
                                       INNER JOIN motorista m ON dt.motorista = m.id_motorista
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario WHERE modelo LIKE '%" + modelo + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar relatorio por VEÍCULO:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
    }
}
