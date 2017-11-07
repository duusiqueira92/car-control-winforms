using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class VeiculoAcessaDados
    {
        CriaBancoAcessoDados db;
        //metodo para cadastrar veiculo
        public void CadastroVeiculo(DTOVeiculos dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();
                string modelo = dto.Modelo.Replace("'", "''");

                db.Conectar();
                string comando = @"INSERT INTO veiculo(modelo, placa, ano, data_cadastro, tipo_combustivel, km_veiculo) 
                                 VALUES('" + modelo + "','" + dto.Placa + "', '" + dto.Ano + "', '" + dto.Data_cadastro + 
                                 "', '" + dto.Tipo_combustivel + "', '" + dto.Km_veiculo + "')";
                db.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar CADASTRAR veículo! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar veiculo
        public void AtualizaVeiculo(DTOVeiculos dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();
                string modelo = dto.Modelo.Replace("'", "''");

                db.Conectar();
                string comando = "UPDATE veiculo SET modelo = '" + modelo + "', placa = '" + dto.Placa + "' , ano = '" + dto.Ano + "', data_cadastro = '" + dto.Data_cadastro + "', tipo_combustivel = '" + dto.Tipo_combustivel + "', km_veiculo = '" + dto.Km_veiculo + "' WHERE id_veiculo = " + dto.Id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar ATUALIZAR o veículo! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para excluir veiculo
        public void ExcluiVeiculo(int id)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = "DELETE FROM veiculo WHERE id_veiculo = " + id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar deletar o VEÍCULO! " + ex.Message);
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
            try
            {
                db = new CriaBancoAcessoDados();
                db.Conectar();

                dt = db.RetDataTable(@"SELECT * FROM veiculo ORDER BY modelo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os veículos: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisar veiculo por placa
        public DataTable PesquisarVeiculoPlaca(string placa)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT * FROM veiculo WHERE placa LIKE '%" + placa + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar VEÍCULO por PLACA: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisar veiculo por modelo
        public DataTable PesquisarVeiculoModelo(string modelo)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT * FROM veiculo WHERE modelo LIKE '%" + modelo + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar VEÍCULO por MODELO: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //verifica se a placa do veiculo já existe no BD
        public bool ValidaPlaca(string placa, int id)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT id_veiculo, placa FROM veiculo WHERE placa = '" + placa + "' && id_veiculo != '" + id + "'";
                dt = db.RetDataTable(comando);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar PLACA: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return false;
        }
    }
}
