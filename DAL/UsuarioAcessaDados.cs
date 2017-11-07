using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using DTO;
using System.Windows.Forms;

namespace DAL
{
    public class UsuarioAcessaDados
    {
        CriaBancoAcessoDados db;
        //fazer o login
        public DataTable Login(string login, string senha)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT id_usuario, nome_usuario, senha FROM usuario WHERE nome_usuario = '" + login + "' AND senha = '" + senha +"'";
                dt = db.RetDataTable(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao LOGAR: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //cadastrar login no BD
        public void CadastroLogin(DTOUsuario dto) 
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"INSERT INTO usuario(nome_usuario, data_cadastro, senha, 
                                    id_usuario) VALUES('" + dto.Nome_usuario + "', '" + dto.Data_cadastro + "', '" + dto.Senha +
                                    "', '" + dto.Id_usuario + "')";
                db.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar CADASTRAR login! " + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //excluir login
        public void ExcluiLogin(int id)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = "DELETE FROM usuario WHERE id_login = " + id;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar deletar o LOGIN" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //retornar um datatable com os funcionarios cadastrados
        public DataTable ListaFuncionario()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_funcionario, nome FROM funcionario WHERE login_cadastrado = 0");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os funcionarios:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }
        //atualizar usuarios
        public void AtualizaUsuario(DTOUsuario dto)
        {
            try
            {
                db = new CriaBancoAcessoDados();

                db.Conectar();
                string comando = @"UPDATE funcionario SET login_cadastrado = '" + dto.Login_cadastrado +
                     "' WHERE id_funcionario = " + dto.Id_usuario;
                db.ExecutarComandoSQL(comando);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar atualizar o Login do usuario" + ex.Message);
            }

            finally
            {
                db = null;
            }
        }
        //retornar um datatable com os logins cadastrados
        public DataTable ListaLoginCadastrado()
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                dt = db.RetDataTable("SELECT id_login, nome_usuario, data_cadastro, id_usuario FROM usuario");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mostrar os logins:" + ex.Message);
            }
            finally
            {
                db = null;
            }
            return dt;
        }

        public bool ValidaUsuario(string usuario)
        {
            DataTable dt = new DataTable();
            db = new CriaBancoAcessoDados();
            try
            {
                db.Conectar();
                string comando = "SELECT nome_usuario FROM usuario WHERE nome_usuario = '" + usuario + "'";
                dt = db.RetDataTable(comando);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar USUÁRIO: " + ex.Message);
            }
            finally
            {
                db = null;
            }
            return false;
        }
    }
}
