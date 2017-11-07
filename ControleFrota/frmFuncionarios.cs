using DAL;
using DTO;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace ControleFrota
{
    public partial class frmFuncionarios : Form
    {
        DTOfuncionario dto;
        FuncionarioAcessaDados dal;

        public frmFuncionarios()
        {
            InitializeComponent();
        }
        //salva ou atualiza informações no bd
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaDados())
            {
                dal = new FuncionarioAcessaDados();
                dto = new DTOfuncionario();
                dto.Nome = txtNome.Text.Trim();
                dto.Bairro = txtBairro.Text.Trim();
                dto.Cargo = Convert.ToInt32(cmbCargo.SelectedValue);
                dto.Celular = txtCelular.Text.Trim();
                dto.Cep = txtCep.Text.Trim();
                dto.Cidade = txtCidade.Text.Trim();
                dto.Cpf = txtCpf.Text.Trim();
                dto.DataCadastro = lblCadastro.Text;
                dto.DataNascimento = txtNascimento.Text;
                dto.Endereco = txtEndereco.Text.Trim();
                dto.Numero = txtNumero.Text.Trim();
                dto.Rg = txtRg.Text.Trim();
                dto.Telefone = txtTelefone.Text.Trim();


                //se o registro for 0
                if (txtRegistro.Text == "0")
                {
                    //cadastra novo funcionario
                    dal.CadastroFuncionario(dto);
                    MessageBox.Show("Funcionário CADASTRADO com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //atualiza funcionario existente
                    dto.Id = int.Parse(txtRegistro.Text);
                    dal.AtualizaFuncionario(dto);
                    MessageBox.Show("Funcionário ATUALIZADO com sucesso!", "Atualizado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ListarFuncionarios();
                PreencherCmbCargo();
                Limpar();
            }
        }
        //Método responsável por limpar os componentes do formulário.
        private void Limpar()
        {
            txtRegistro.Text = "0";
            txtNome.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCep.Clear();
            txtCidade.Clear();
            txtNumero.Clear();
            txtNascimento.Text = String.Empty;
            cmbCargo.SelectedIndex = -1;
            txtRg.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
            txtCelular.Clear();
        }
        //Método que realiza o intervalo de cores dentro do DataGriView.
        private void Estilo()
        {
            for (int i = 0; i < dtgFuncionarios.Rows.Count; i += 2)
            {
                dtgFuncionarios.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //Método que lista os dados da tabela Funcionarios no DataGridView.
        private void ListarFuncionarios()
        {
            dal = new FuncionarioAcessaDados();
            dtgFuncionarios.DataSource = dal.SelecionaTodosFuncionarios();
            Estilo();
        }
        //ao carregar o formulario, aciona os metodos
        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            ListarFuncionarios();
            PreencherCmbCargo();
            lblCadastro.Text = DateTime.Now.ToShortDateString();
        }
        //preenche o cmbCargo com valores da tabela cargo
        private void PreencherCmbCargo()
        {
            cmbCargo.DataSource = dal.ListaCargos();
            cmbCargo.DisplayMember = "nome_cargo";
            cmbCargo.ValueMember = "id_cargo";

            cmbCargo.SelectedIndex = -1;
        }
        //Limpa todos os campos do formulario
        private void tsNovo_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        //pesquisa de funcionario
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            /* Evento do txtPesquisa, o qual verifica se desejamos pesquisar por nome ou CPF
             * e exibe os resultados de acordo com o que for digitado nele. */
            dal = new FuncionarioAcessaDados();

            try
            {
                if (rbNome.Checked)
                {
                    dtgFuncionarios.DataSource = dal.PesquisarFuncionarioNome(txtPesquisa.Text);
                }
                else if (rbCpf.Checked)
                {
                    dtgFuncionarios.DataSource = dal.PesquisarFuncionarioCpf(txtPesquisa.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //evento click de celula no dgv
        private void dtgFuncionarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //se clicar no btnEditar, retorna as linhas para o formulario
                if (dtgFuncionarios.Columns[e.ColumnIndex].Name == "btnEditar")
                {
                    txtRegistro.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["id_funcionario"].Value.ToString();
                    txtNome.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                    txtEndereco.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["endereco"].Value.ToString();
                    txtCidade.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["cidade"].Value.ToString();
                    txtBairro.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["bairro"].Value.ToString();
                    txtNumero.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["numero"].Value.ToString();
                    txtNascimento.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["nascimento"].Value.ToString();
                    txtCep.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["cep"].Value.ToString();
                    txtRg.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["rg"].Value.ToString();
                    txtCpf.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["cpf"].Value.ToString();
                    txtTelefone.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["telefone"].Value.ToString();
                    txtCelular.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["celular"].Value.ToString();
                    cmbCargo.Text = dtgFuncionarios.Rows[e.RowIndex].Cells["nome_cargo"].Value.ToString();
                }
                //exclui a linha selecionada do banco de dados
                else if (dtgFuncionarios.Columns[e.ColumnIndex].Name == "btnDeletar" && MessageBox.Show("Deseja realmente excluir?", "Deseja excluir?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dal.ExcluiFuncionario(Convert.ToInt32(dtgFuncionarios.Rows[e.RowIndex].Cells["id_funcionario"].Value));
                    MessageBox.Show("Funcionário EXCLUÍDO com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    ListarFuncionarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o FUNCIONÁRIO!" + ex.Message, "Erro 0002");
            }
        }
        //validação dos campos
        private Boolean validaDados()
        {
            if (txtCpf.Text.Length < 14)
            {
                MessageBox.Show("Complete o campo CPF", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCpf.Focus();
                return false;
            }
            if (dal.ValidaCpf(txtCpf.Text, Convert.ToInt32(txtRegistro.Text)))
            {
                MessageBox.Show("CPF já cadastrado!", "ERRO 0001");
                txtCpf.Focus();
                return false;
            }

            if (txtNome.Text == string.Empty)
            {
                MessageBox.Show("Verifique o NOME, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }
            if (txtEndereco.Text == string.Empty)
            {
                MessageBox.Show("Verifique o ENDEREÇO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndereco.Focus();
                return false;
            }
            if (txtBairro.Text == string.Empty)
            {
                MessageBox.Show("Verifique o BAIRRO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBairro.Focus();
                return false;
            }
            if (txtCidade.Text == string.Empty)
            {
                MessageBox.Show("Verifique a CIDADE, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCidade.Focus();
                return false;
            }
            if (txtCep.Text == string.Empty)
            {
                MessageBox.Show("Verifique o CEP, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCep.Focus();
                return false;
            }
            if (txtNumero.Text == string.Empty)
            {
                MessageBox.Show("Verifique o NÚMERO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }
            if (txtTelefone.Text == string.Empty)
            {
                MessageBox.Show("Verifique o TELEFONE, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return false;
            }
            if (txtNascimento.Text == string.Empty)
            {
                MessageBox.Show("Verifique o NASCIMENTO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNascimento.Focus();
                return false;
            }

            if (txtRg.Text == string.Empty)
            {
                MessageBox.Show("Verifique o RG, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRg.Focus();
                return false;
            }

            if (cmbCargo.Text == string.Empty)
            {
                MessageBox.Show("Verifique o CARGO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargo.Focus();
                return false;
            }
            return true;
        }
        //evento para retornar ao menu principal
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
