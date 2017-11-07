using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DAL
{
    public class MotoristaAcessaDados
    {
        CriaBancoAcessoDados db;
        //metodo para cadastra motorista
        public void CadastroMotorista(DTOMotorista dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"INSERT INTO motorista(id_funcionario, cnh, registro_cnh, categoria_cnh, 
                                    validade_cnh, data_cadastro) VALUES('" + dto.Id_Funcionario + 
                                    "', '" + dto.Cnh + "', '" + dto.Registro_Cnh + "', '" + dto.Categoria + 
                                    "', '" + dto.Validade + "', '" + dto.Cadastro  + "')";
                db.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar CADASTRAR motorista! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar o campo motorista_cadastrado
        public void AtualizaCampoMotoristaCadastrado(DTOMotorista dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE funcionario SET motorista_cadastrado = '" + dto.Motorista_cadastro +
                     "' WHERE id_funcionario = " + dto.Id_Funcionario;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar Motorista_cadastrado do funcionario" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        
        public void ExcluiCampoMotoristaCadastrado(DTOMotorista dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE funcionario SET motorista_cadastrado = '" + dto.Motorista_cadastro +
                     "' WHERE id_funcionario = " + dto.Id_Funcionario ;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar Motorista_cadastrado do funcionario" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para atualizar motorista
        public void AtualizaMotorista(DTOMotorista dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE motorista SET id_funcionario = '" + dto.Id_Funcionario + 
                    "', cnh = '" + dto.Cnh + "', registro_cnh = '" + dto.Registro_Cnh + 
                    "', categoria_cnh = '" + dto.Categoria + "', validade_cnh = '" + dto.Validade + 
                    "', data_cadastro = '" + dto.Cadastro + "' where id_motorista = " + dto.Id_Motorista;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar ATUALIZAR motorista! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para excluir motorista
        public void ExcluiMotorista(int id)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = "DELETE FROM motorista WHERE id_motorista = " + id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar deletar MOTORISTA! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //metodo para retornar todos os funcionarios cadastrados como motorista
        public DataTable SelecionaTodosFuncionarios()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_funcionario, nome FROM funcionario WHERE CARGO = 1 && motorista_cadastrado = 0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Motoristas: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //metodo para retornar categoria da cnh
        public DataTable ListaCategoria()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_categoria, categoria FROM categoria");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os CARGOS: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //retorna os motoristas cadastrados
        public DataTable SelecionaTodosMotoristas()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT f.nome, c.categoria, m.id_funcionario, m.id_motorista, m.cnh, m.registro_cnh, m.validade_cnh, m.data_cadastro FROM motorista m 
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario 
                                       INNER JOIN categoria c on  m.categoria_cnh = c.id_categoria WHERE motorista_ativo = 1 order by f.nome");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os Motoristas: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisa por cnh do motorista
        public DataTable PesquisarMotoristaCnh(string cnh)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT f.nome, c.categoria, m.id_funcionario, m.id_motorista, m.cnh, m.registro_cnh, m.validade_cnh, m.data_cadastro FROM motorista m 
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario 
                                       INNER JOIN categoria c on  m.categoria_cnh = c.id_categoria WHERE cnh LIKE '%" + cnh + "%'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar MOTORISTA por CNH: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //pesquisa por nome do motorista
        public DataTable PesquisarMotoristaNome(string nome)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable(@"SELECT f.nome, c.categoria, m.id_funcionario, m.id_motorista, m.cnh, m.registro_cnh, m.validade_cnh, m.data_cadastro FROM motorista m 
                                       INNER JOIN funcionario f ON m.id_funcionario = f.id_funcionario 
                                       INNER JOIN categoria c on  m.categoria_cnh = c.id_categoria WHERE nome LIKE '%" + nome + "%'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar MOTORISTA por NOME: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //verifica se o numero da cnh já está cadastrada
        public bool ValidaCnh(string cnh)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT cnh FROM motorista WHERE cnh = '" + cnh + "'";
                dt = db.RetDataTable(comando);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar CNH: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return false;
        }
        //verifica se o registro_cnh ja esta cadastrado
        public bool ValidaRegCnh(string reg_cnh)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT registro_cnh FROM motorista WHERE registro_cnh = '" + reg_cnh + "'";
                dt = db.RetDataTable(comando);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar CNH: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return false;
        }
    }
}
