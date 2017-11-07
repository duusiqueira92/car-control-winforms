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
    public partial class frmVeiculos : Form
    {
        DTOVeiculos dto;
        VeiculoAcessaDados dal;
        PercursoAcessaDados dalPercurso;

        public frmVeiculos()
        {
            InitializeComponent();
        }
        //evento click salvar do formulario
        private void tsSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaDados())
            {
                dal = new VeiculoAcessaDados();
                dto = new DTOVeiculos();
                dto.Modelo = txtModelo.Text.Trim();
                dto.Ano = cmbAno.Text.Trim();
                dto.Km_veiculo = Convert.ToInt32(txtKm.Text);
                dto.Placa = txtPlaca.Text.Trim();
                dto.Tipo_combustivel = Convert.ToInt32(cmbCombustivel.SelectedValue);
                dto.Data_cadastro = lblCadastro.Text;

                //se o txtRegistro for igual a 0
                if (txtRegistro.Text == "0")
                {
                    //cadastra novo veículo
                    dal.CadastroVeiculo(dto);
                    MessageBox.Show("Veículo CADASTRADO com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //se o txtRegistro for diferente de 0
                else
                {
                    //atualiza o cadastro do veículo
                    dto.Id = int.Parse(txtRegistro.Text);
                    dal.AtualizaVeiculo(dto);
                    MessageBox.Show("Veículo ATUALIZADO com sucesso!", "Atualizado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ListarVeiculo();
                PreencherCmbCombustivel();
                Limpar();
                Estilo();
              
            }
        }
        //limpar todos os campos do fomulario
        private void Limpar()
        {
            
            txtRegistro.Text = "0";
            txtModelo.Clear();
            txtPlaca.Clear();
            cmbAno.SelectedIndex = -1;
            cmbCombustivel.SelectedIndex = -1;
            txtPesquisa.Clear();
            rbModelo.Checked = true;
            txtKm.Clear();
        }
        //coloca estilo de cor no data grid view
        private void Estilo()
        {
            for (int i = 0; i < dtgVeiculos.Rows.Count; i += 2)
            {
                dtgVeiculos.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //preenche o combobox com dados do BD
        public void PreencherCmbCombustivel()
        {
            dalPercurso = new PercursoAcessaDados();
            cmbCombustivel.DataSource = dalPercurso.SelecionaTodosCombustiveis(); ;
            cmbCombustivel.DisplayMember = "nome_combustivel";
            cmbCombustivel.ValueMember = "id_combustivel";

            cmbCombustivel.SelectedIndex = -1;
        }
        //preenche o data grid view com dados do DB
        private void ListarVeiculo()
        {
            dal = new VeiculoAcessaDados();
            dtgVeiculos.DataSource = dal.SelecionaTodosVeiculos();
            Estilo();
        }
        //evento iniciado ao carregar o formulario
        private void frmVeiculos_Load(object sender, EventArgs e)
        {
            //lança a data atual a um label
            lblCadastro.Text = DateTime.Now.ToShortDateString();

            Limpar();
            ListarVeiculo();
            PreencherCmbCombustivel();
        }
        //evento click de celular do data grid view
        private void dtgVeiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //se clicar no btnEditar, retorna as linhas para o formulario
                if (dtgVeiculos.Columns[e.ColumnIndex].Name == "btnEditar")
                {
                    txtRegistro.Text = dtgVeiculos.Rows[e.RowIndex].Cells["id_veiculo"].Value.ToString();
                    txtModelo.Text = dtgVeiculos.Rows[e.RowIndex].Cells["modelo"].Value.ToString();
                    txtKm.Text = dtgVeiculos.Rows[e.RowIndex].Cells["km_veiculo"].Value.ToString();
                    txtPlaca.Text = dtgVeiculos.Rows[e.RowIndex].Cells["placa"].Value.ToString();
                    cmbCombustivel.SelectedValue = dtgVeiculos.Rows[e.RowIndex].Cells["tipo_combustivel"].Value.ToString();
                    cmbAno.SelectedItem = dtgVeiculos.Rows[e.RowIndex].Cells["ano"].Value.ToString();
                    lblCadastro.Text = dtgVeiculos.Rows[e.RowIndex].Cells["data_cadastro"].Value.ToString();
                }

                //exclui a linha selecionada do banco de dados
                else if (dtgVeiculos.Columns[e.ColumnIndex].Name == "btnDeletar" && MessageBox.Show("Deseja realmente excluir?", "Deseja excluir?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dal.ExcluiVeiculo(Convert.ToInt32(dtgVeiculos.Rows[e.RowIndex].Cells["id_veiculo"].Value));
                    MessageBox.Show("Veículo EXCLUÍDO com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ListarVeiculo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir VEÍCULO!" + ex.Message, "Erro 0002");
            }
        }
        //evento para pesquisa de veiculos
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            /* Evento do txtPesquisa, o qual verifica se desejamos pesquisar por nome ou CPF
             * e exibe os resultados de acordo com o que for digitado nele. */
            dal = new VeiculoAcessaDados();

            try
            {
                //pesquisa pelo modelo
                if (rbModelo.Checked)
                {
                    dtgVeiculos.DataSource = dal.PesquisarVeiculoModelo(txtPesquisa.Text);
                }
                //pesquisa pela placa
                else if (rbPlaca.Checked)
                {
                    dtgVeiculos.DataSource = dal.PesquisarVeiculoPlaca(txtPesquisa.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //limpar form
        private void tsNovo_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        //validacao dos campos do formulario
        private Boolean ValidaDados()
        {
            if (txtPlaca.Text.Length < 8)
            {
                MessageBox.Show("Verifique o campo PLACA!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPlaca.Focus();
                return false;
            }

            if (dal.ValidaPlaca(txtPlaca.Text, Convert.ToInt32(txtRegistro.Text)))
            {
                MessageBox.Show("Placa já cadastrada", "Erro 0001");
                txtPlaca.Focus();
                return false;
            }
            

            if (txtKm.Text == string.Empty)
            {
                MessageBox.Show("Verifique o KM, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKm.Focus();
                return false;
            }

            if (txtModelo.Text == string.Empty)
            {
                MessageBox.Show("Verifique o MODELO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModelo.Focus();
                return false;
            }

            if (txtPlaca.Text == string.Empty)
            {
                MessageBox.Show("Verifique o PLACA, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlaca.Focus();
                return false;
            }

            if (cmbAno.Text == string.Empty)
            {
                MessageBox.Show("Verifique o ANO, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAno.Focus();
                return false;
            }

            if (cmbCombustivel.Text == string.Empty)
            {
                MessageBox.Show("Verifique o COMBUSTÍVEL, por favor!", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCombustivel.Focus();
                return false;
            }
            return true;
        }
        //voltar ao menu anterior
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
