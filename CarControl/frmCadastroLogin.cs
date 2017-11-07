using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleFrota
{
    public partial class frmCadastroLogin : Form
    {
        UsuarioAcessaDados dalUsuario;
        DTOUsuario dto;
        Criptografia senhaCripto;
        public frmCadastroLogin()
        {
            InitializeComponent();
        }
        //evento load do form
        private void frmCadastroLogin_Load(object sender, EventArgs e)
        {
            lblDataCadastro.Text = DateTime.Now.ToShortDateString();
            PreencherCmbFuncionario();
            dgvLogin.DataSource = dalUsuario.ListaLoginCadastrado();
            Estilo();
        }
        //fechar o form e voltar ao menu principal
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            this.Close();
            frm.ShowDialog();
        }
        //salvar os dados
        private void tsSalvar_Click(object sender, EventArgs e)
        {
            //validar campos
            if (validaDados())
            {
                senhaCripto = new Criptografia();
                dalUsuario = new UsuarioAcessaDados();
                dto = new DTOUsuario();
                dto.Nome_usuario = txtLogin.Text.Trim();
                dto.Senha = senhaCripto.Criptografar(txtSenha.Text.Trim());
                dto.Data_cadastro = lblDataCadastro.Text;
                dto.Id_usuario = Convert.ToInt32(cmbFuncionario.SelectedValue);
                dto.Login_cadastrado = 1;
            
                //se o registro for igual a 0, gera um novo cadastro
                if (txtRegistro.Text == "0")
                {
                    dalUsuario.CadastroLogin(dto);
                    dalUsuario.AtualizaUsuario(dto);
                    MessageBox.Show("Login cadastrado com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Limpar();
                PreencherCmbFuncionario();
                ListarUsuario();
            }
        }
        //evento para apagar todos os campos dos forms
        private void Limpar()
        {
            cmbFuncionario.SelectedValue = -1;
            txtLogin.Text = String.Empty;
            txtSenha.Text = String.Empty;
            txtSenha2.Text = String.Empty;
            txtRegistro.Text = "0";
        }
        //dar cores intercaladas as tuplas do dgv
        private void Estilo()
        {
            for (int i = 0; i < dgvLogin.Rows.Count; i += 2)
            {
                dgvLogin.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //listar usuarios cadastrados
        private void ListarUsuario()
        {
            dgvLogin.DataSource = dalUsuario.ListaLoginCadastrado();
            Estilo();
        }
        //validação de dados
        private Boolean validaDados()
        {
            if (dalUsuario.ValidaUsuario(txtLogin.Text))
            {
                MessageBox.Show("Login cadastrado!", "Erro 0001", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }
            if (txtLogin.Text == string.Empty)
            {
                MessageBox.Show("Verifique o LOGIN, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }
            if (txtSenha.Text == string.Empty)
            {
                MessageBox.Show("Verifique a SENHA, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }
            if (txtSenha2.Text == string.Empty)
            {
                MessageBox.Show("Verifique o campo REDIGITE A SENHA, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha2.Focus();
                return false;
            }
            if (txtSenha.Text != txtSenha2.Text)
            {
                MessageBox.Show("As SENHAS tem que ser iguais!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }
            return true;
        }
        //preenche o cmgFuncionario
        private void PreencherCmbFuncionario()
        {
            dalUsuario = new UsuarioAcessaDados();
            cmbFuncionario.DataSource = dalUsuario.ListaFuncionario();
            cmbFuncionario.DisplayMember = "nome";
            cmbFuncionario.ValueMember = "id_funcionario";

            cmbFuncionario.SelectedIndex = -1;
        }
        //evento click do dgv
        private void dgvLogin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dto = new DTOUsuario();
                dalUsuario = new UsuarioAcessaDados();
                //exclui a linha selecionada do banco de dados
                if (dgvLogin.Columns[e.ColumnIndex].Name == "btnExcluir" && MessageBox.Show("Deseja realmente excluir?", "Deseja excluir?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dalUsuario.ExcluiLogin(Convert.ToInt32(dgvLogin.Rows[e.RowIndex].Cells["id_login"].Value));
                    dto.Login_cadastrado = 0; //ativa o motorista para ser mostrado no cmbMotorista
                    dto.Id_usuario = Convert.ToInt32(dgvLogin.Rows[e.RowIndex].Cells["id_usuario"].Value);
                    dalUsuario.AtualizaUsuario(dto);
                    MessageBox.Show("Login EXCLUÍDO com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dalUsuario.ListaLoginCadastrado();
                dalUsuario.ListaFuncionario();
                ListarUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao EXCLUIR login! " + ex.Message, "Erro 0002");
            }
        }
    }
}
