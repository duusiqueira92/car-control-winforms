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
    public partial class frmMotorista : Form
    {
        MotoristaAcessaDados dalMotorista;
        DTOMotorista dto;
        public frmMotorista()
        {
            InitializeComponent();
        }
        //limpa todos os campos dos formulario
        private void tsNovo_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        //salva no banco de dados
        private void tsSalvar_Click(object sender, EventArgs e)
        {
            //se a condição for verdadeira
            if (ValidaDados())
            {
                dalMotorista = new MotoristaAcessaDados();
                dto = new DTOMotorista();
                dto.Id_Funcionario = Convert.ToInt32(cmbFuncionario.SelectedValue);
                dto.Cnh = txtCnh.Text.Trim();
                dto.Registro_Cnh = txtRegCnh.Text.Trim();
                dto.Categoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                dto.Validade = txtValidade.Text;
                dto.Cadastro = lblCadastro.Text;
                dto.Motorista_cadastro = 1;
                //se o registro for 0
                if (txtRegistro.Text == "0")
                {
                    //cadastra um novo motorista
                    dalMotorista.CadastroMotorista(dto);
                    dalMotorista.AtualizaCampoMotoristaCadastrado(dto);
                    MessageBox.Show("Motorista cadastrado com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //se for != 0
                else
                {
                    //atualiza dados do motorista selecionado
                    dto.Id_Motorista = int.Parse(txtRegistro.Text);
                    dalMotorista.AtualizaMotorista(dto);
                    MessageBox.Show("Motorista atualizado com sucesso!", "Atualizado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Limpar();
                ListarMotorista();
                PreencherCategoria();
                PreencherCmbFuncionario();
            }
        }
        //volta ao menu principal
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //carrega todas as informações ao abrir o form
        private void frmTeste_Load(object sender, EventArgs e)
        {
            lblCadastro.Text = DateTime.Now.ToShortDateString();
            Limpar();
            PreencherCmbFuncionario();
            PreencherCategoria();
            ListarMotorista();
        }
        //limpa os campos
        private void Limpar()
        {
            txtRegistro.Text = "0";
            txtValidade.Text = String.Empty;
            txtRegCnh.Text = String.Empty;
            txtPesquisa.Text = String.Empty;
            txtCnh.Text = String.Empty;
            cmbCategoria.SelectedIndex = -1;
            cmbFuncionario.SelectedIndex = -1;
            rbNome.Checked = true;
        }
        //lista os motoristas no dgv
        private void ListarMotorista()
        {
            dtgMotorista.DataSource = dalMotorista.SelecionaTodosMotoristas();
            Estilo();
        }
        //insere cores intercalandos as tuplas do dgv
        private void Estilo()
        {
            for (int i = 0; i < dtgMotorista.Rows.Count; i += 2)
            {
                dtgMotorista.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //retorna os funcionarios com cargo de motorista no combobox
        public void PreencherCmbFuncionario()
        {
            dalMotorista = new MotoristaAcessaDados();
            cmbFuncionario.DataSource = dalMotorista.SelecionaTodosFuncionarios();
            cmbFuncionario.DisplayMember = "nome";
            cmbFuncionario.ValueMember = "id_funcionario";

            cmbFuncionario.SelectedIndex = -1;
        }
        //preenche o combobox com as categorias de cnh
        public void PreencherCategoria()
        {
            cmbCategoria.DataSource = dalMotorista.ListaCategoria();
            cmbCategoria.DisplayMember = "categoria";
            cmbCategoria.ValueMember = "id_categoria";

            cmbCategoria.SelectedIndex = -1;
        }
        //evento click do dgv
        private void dtgMotorista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //exclui a linha selecionada do banco de dados
                if (dtgMotorista.Columns[e.ColumnIndex].Name == "btnDeletar" && MessageBox.Show("Deseja realmente excluir?", "Deseja excluir?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dto = new DTOMotorista();
                    dto.Id_Funcionario = (Convert.ToInt32(dtgMotorista.Rows[e.RowIndex].Cells["id_funcionario"].Value));
                    dto.Motorista_cadastro = 0;
                    dalMotorista.ExcluiMotorista(Convert.ToInt32(dtgMotorista.Rows[e.RowIndex].Cells["id_motorista"].Value));
                    dalMotorista.ExcluiCampoMotoristaCadastrado(dto);
                    MessageBox.Show("Motorista EXCLUÍDO com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                PreencherCmbFuncionario();
                ListarMotorista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o MOTORISTA! " + ex.Message, "Erro 0002");
            }
        }
        //pesquisa por parametro
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            /* Evento do txtPesquisa, o qual verifica se desejamos pesquisar por nome ou CPF
            * e exibe os resultados de acordo com o que for digitado nele. */
            dalMotorista = new MotoristaAcessaDados();

            try
            {
                if (rbCnh.Checked)
                {
                    dtgMotorista.DataSource = dalMotorista.PesquisarMotoristaCnh(txtPesquisa.Text);
                }
                else if (rbNome.Checked)
                {
                    dtgMotorista.DataSource = dalMotorista.PesquisarMotoristaNome(txtPesquisa.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //validação de campos
        private Boolean ValidaDados()
        {
            if (dalMotorista.ValidaCnh(txtCnh.Text))
            {
                MessageBox.Show("CNH já cadastrada!", "Erro 0001", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCnh.Focus();
                return false;
            }
            if (dalMotorista.ValidaRegCnh(txtRegCnh.Text))
            {
                MessageBox.Show("Reg. CNH já cadastrada!", "Erro 0001", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegCnh.Focus();
                return false;
            }
            if (txtCnh.Text == string.Empty)
            {
                MessageBox.Show("Verifique a CNH, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCnh.Focus();
                return false;
            }

            if (txtRegCnh.Text == string.Empty)
            {
                MessageBox.Show("Verifique o REGISTRO CNH, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegCnh.Focus();
                return false;
            }

            if (txtValidade.Text == string.Empty)
            {
                MessageBox.Show("Verifique a VALIDADE, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValidade.Focus();
                return false;
            }

            if (cmbCategoria.Text == string.Empty)
            {
                MessageBox.Show("Verifique a CATEGORIA, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return false;
            }

            if (cmbFuncionario.Text == string.Empty)
            {
                MessageBox.Show("Verifique o FUNCIONÁRIO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFuncionario.Focus();
                return false;
            }

            return true;
        }
    }
}
