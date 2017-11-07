using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class PercursoAcessaDados
    {
        CriaBancoAcessoDados db;
        //metodo para cadastrar percurso
        public void CadastroPercurso(DTOPercurso dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                //string comando = "INSERT INTO dirige_trajeto(id_trajeto, abastecido, periodo_inicial, periodo_final, combustivel_abastecimento, motorista, litros_abastecidos, veiculo ) VALUES('" + dto.Id_trajeto + "', '" + dto.Abastecido + "', '" + dto.Periodo_inicial + "', '" + dto.Periodo_final + "', '" + dto.Combustivel_abastecimento + "', '" + dto.Motorista + "', '" + dto.Litros_abastecido + "', '" + dto.Veiculo + "')";
                string comando = @"INSERT INTO dirige_trajeto(id_trajeto, periodo_inicial, 
                                   motorista, veiculo, km_inicio, hora_inicio ) VALUES('" + dto.Id_trajeto + 
                                   "', '" + dto.Periodo_inicial + "','" + dto.Motorista + "', '" + dto.Veiculo + "', '" + 
                                   dto.Km_veiculo + "', '" + dto.Hora_inicial + "')";
                db.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar CADASTRAR percurso! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //habilita veiculo para o combobox
        public void AtualizaVeiculo(DTOPercurso dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE veiculo SET veiculo_ativo = '" + dto.Veiculo_Ativo +
                     "' WHERE id_veiculo = " + dto.Veiculo;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o Percurso" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //habilita motorista no combobox
        public void AtualizaMotorista(DTOPercurso dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE motorista SET motorista_ativo = '" + dto.Motorista_Ativo +
                     "' where id_motorista = " + dto.Motorista;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o Percurso" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar percurso
        public void AtualizaTrajeto(DTOPercurso dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE dirige_trajeto SET id_trajeto = '" + dto.Id_trajeto +
                    "', periodo_inicial = '" + dto.Periodo_inicial +
                    "', hora_inicio = '" + dto.Hora_inicial +
                    "', periodo_final = '" + dto.Periodo_final + "', km_inicio = '" + dto.Km_veiculo + 
                    "', motorista = '" + dto.Motorista + 
                    "', veiculo ='" + dto.Veiculo + "' where id_trajeto = " + dto.Id_trajeto;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o Percurso" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar data_fim e km_fim
        public void AtualizaFimTrajeto(DTOPercurso dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                //veiculo ativo
                string comando = @"UPDATE dirige_trajeto 
                                    INNER JOIN veiculo ON veiculo.id_veiculo = dirige_trajeto.veiculo
                                    INNER JOIN motorista ON motorista.id_motorista = dirige_trajeto.motorista
                                    SET id_trajeto = '" + dto.Id_trajeto +
                                    "', km_veiculo = '" + dto.Km_veiculo + "' , abastecido = '" + dto.Abastecido + 
                                    "', periodo_final = '" + dto.Periodo_final +
                                    "', hora_fim = '" + dto.Hora_final + "', km_fim = '" + dto.Km_veiculo +
                                    "', combustivel_abastecimento = '" + dto.Combustivel_abastecimento + 
                                    "', motorista = '" + dto.Motorista +
                                    "', ativo = '" + dto.Ativo + "', veiculo_ativo = '" + dto.Veiculo_Ativo +
                                    "', litros_abastecidos = '" + dto.Litros_abastecido + "', motorista_ativo = '" + dto.Motorista_Ativo +
                                    "', veiculo ='" + dto.Veiculo + "' where id_trajeto = " + dto.Id_trajeto;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o Percurso" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para excluir trajeto
        public void ExcluiTrajeto(int id)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = "DELETE FROM dirige_trajeto WHERE id_trajeto = " + id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar deletar o TRAJETO" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //retorna todos os veiculos para um ComboBox
        public DataTable SelecionaTodosVeiculos()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_veiculo, modelo, placa, ano, km_veiculo FROM veiculo WHERE veiculo_ativo = 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os VEÍCULOS: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //retorna todos os combustiveis para um ComboBox
        public DataTable SelecionaTodosCombustiveis()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_combustivel, nome_combustivel FROM tipo_combustivel WHERE id_combustivel BETWEEN 1 AND 3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Combustivéis:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //retorna todos os percursos para o data grid view
        public DataTable SelecionaTodosPercursos()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                 dt = db.RetDataTable(@"SELECT id_trajeto, id_motorista, id_veiculo, nome, modelo, periodo_inicial, hora_inicio, periodo_final, 
                                       km_inicio, km_fim, abastecido, nome_combustivel, 
                                       litros_abastecidos, km_veiculo FROM dirige_trajeto dt
                                       INNER JOIN veiculo v ON dt.veiculo = v.id_veiculo
                                       INNER JOIN tipo_combustivel tc ON dt.combustivel_abastecimento = tc.id_combustivel
                                       INNER JOIN motorista m ON dt.motorista = m.id_motorista
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario WHERE ativo = 1 ORDER BY nome");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Motoristas ativos:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
    }
}
