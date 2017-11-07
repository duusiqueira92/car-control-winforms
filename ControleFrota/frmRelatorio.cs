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
    public partial class frmRelatorio : Form
    {
        RelatorioAcessaDados relatorio = new RelatorioAcessaDados();

        public frmRelatorio()
        {
            InitializeComponent();
        }
        //evento load do formulario
        private void frmRelatorio_Load(object sender, EventArgs e)
        {
            dgvDetalhes.DataSource = relatorio.SelecionaTodosTrajetos();
        }
        //preenche o cmbSelecao com todos os motoristas cadastrados
        private void rbMotorista_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMotorista.Checked)
            {
                cmbSelecao.Visible = true;
                cmbSelecao.DataSource = relatorio.SelecionaTodosMotoristas();
                cmbSelecao.DisplayMember = "nome";
                cmbSelecao.ValueMember = "id_motorista";

                cmbSelecao.SelectedValue = -1;
            }

        }
        //preenche o cmbSelecao com todos os veiculos cadastrados
        private void rbVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVeiculo.Checked)
            {
                cmbSelecao.Visible = true;
                cmbSelecao.DataSource = relatorio.SelecionaTodosVeiculos();
                cmbSelecao.DisplayMember = "modelo";
                cmbSelecao.ValueMember = "id_veiculo";

                cmbSelecao.SelectedValue = -1;
            }
        }
        //evento para selecionar relatorio
        private void cmbSelecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //relatorio por motoristas
            if (rbMotorista.Checked && cmbSelecao.SelectedIndex != -1)
            {
                DataRowView drw = ((DataRowView)cmbSelecao.SelectedItem);
                string id_motorista = drw["id_motorista"].ToString();
                dgvDetalhes.DataSource = relatorio.SelecionaRelatorioIndividualMotorista(Convert.ToInt32(id_motorista));
                
            }
            //relatorio por veiculo
            else if (rbVeiculo.Checked && cmbSelecao.SelectedIndex != -1)
            {
                DataRowView drw = ((DataRowView)cmbSelecao.SelectedItem);
                string id_veiculo = drw["id_veiculo"].ToString();
                dgvDetalhes.DataSource = relatorio.SelecionaRelatorioIndividualVeiculo(Convert.ToInt32(id_veiculo));
            }
        }
        //retorna para o dgv todos os trajetos cadastrados
        private void rbTodosRegistros_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodosRegistros.Checked)
            {
                cmbSelecao.Visible = false;
                dgvDetalhes.DataSource = relatorio.SelecionaTodosTrajetos();
                cmbSelecao.DataSource = null;
                cmbSelecao.SelectedValue = -1;
            }

        }
        //volta ao menu principal
        private void btnSair_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (rbPesqMotorista.Checked)
            {
                dgvDetalhes.DataSource = relatorio.PesquisarRelatorioMotorista(txtPesquisa.Text);
            }

            else if (rbPesqVeiculo.Checked)
            {
                dgvDetalhes.DataSource = relatorio.PesquisarRelatorioVeiculo(txtPesquisa.Text);
            }
        }
    }
}
