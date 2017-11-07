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
    public class FuncionarioAcessaDados
    {
        CriaBancoAcessoDados db;
        //metodo para cadastrar funcionarios
        public void CadastroFuncionario(DTOfuncionario dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();
                string nome = dto.Nome.Replace("'", "''");
                string endereco = dto.Endereco.Replace("'", "''");
                string bairro = dto.Bairro.Replace("'", "''");
                string cidade = dto.Cidade.Replace("'", "''");

                db.Conectar();
                string comando = @"INSERT INTO funcionario(nome, endereco, bairro, cidade, 
                                   numero, cep, nascimento, data_cadastro, telefone, celular,
                                   cargo, cpf, rg) VALUES('" + nome + "','" + endereco + 
                                   "', '" + bairro + "', '" + cidade + "', '" + dto.Numero +
                                   "', '" + dto.Cep + "', '" + dto.DataNascimento + "', '" +
                                   dto.DataCadastro + "', '" + dto.Telefone + "', '" + 
                                   dto.Celular + "', '" + dto.Cargo + "', '" + dto.Cpf + 
                                   "', '" + dto.Rg + "')";
                db.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar cadastrar funcionário: " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar funcionarios
        public void AtualizaFuncionario(DTOfuncionario dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();
                string nome = dto.Nome.Replace("'", "''");
                string endereco = dto.Endereco.Replace("'", "''");
                string bairro = dto.Bairro.Replace("'", "''");
                string cidade = dto.Cidade.Replace("'", "''");

                db.Conectar();
                string comando = "UPDATE funcionario SET nome = '" + nome + "', endereco = '" + endereco + "' , bairro = '" + bairro + "', cidade = '" + cidade + "', numero = '" + dto.Numero + "', cep = '" + dto.Cep + "', nascimento = '" + dto.DataNascimento + "', data_cadastro = '" + dto.DataCadastro + "', telefone = '" + dto.Telefone + "', celular = '" + dto.Celular + "', cargo = '" + dto.Cargo + "', cpf = '" + dto.Cpf + "', rg = '" + dto.Rg + "' WHERE id_funcionario = " + dto.Id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o funcionario" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para excluir o funcionario
        public void ExcluiFuncionario(int id)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = "DELETE FROM funcionario WHERE id_funcionario = " + id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar deletar o FUNCIONÁRIO" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para retornar todos os funcionarios para um data grid view
        public DataTable SelecionaTodosFuncionarios()
        {
            DataTable dt = new DataTable();
            try
            {
                db = new CriaBancoAcessoDados();
                db.Conectar();

                dt = db.RetDataTable(@"SELECT f.id_funcionario, f.nome, f.endereco, f.bairro, f.cidade, f.numero, f.cep, f.nascimento, f.data_cadastro, 
                                    f.telefone, f.celular, c.nome_cargo, f.cpf, f.rg FROM funcionario f 
                                    INNER JOIN cargo c ON f.cargo = c.id_cargo ORDER BY f.nome");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os funcionários:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para listar os cargos em um ComboBox
        public DataTable ListaCargos()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_cargo, nome_cargo, descricao_cargo FROM cargo");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os cargos:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para retornar a pesquisa por nome
        public DataTable PesquisarFuncionarioNome(string nome)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT * FROM funcionario WHERE nome LIKE '%"+ nome + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar funcionário por NOME:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para retornar a pesquisa por cpf
        public DataTable PesquisarFuncionarioCpf(string cpf)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT * FROM funcionario WHERE cpf LIKE '%" + cpf + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar funcionário por CPF:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //verifica se o cpf do funcionario ja esta cadastrado
        public bool ValidaCpf(string cpf, int id)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT cpf FROM funcionario WHERE cpf = '" + cpf + "' && id_funcionario != '" + id + "'";
                dt = db.RetDataTable(comando);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar CPF: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return false;
        }
    }
}
