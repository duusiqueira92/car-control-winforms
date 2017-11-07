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
   

    public partial class frmEncerraPercurso : Form
    {
        PercursoAcessaDados dalPercurso;
        DTOPercurso dto;
        int veiculo, motorista, km_inicio;
        //iniciando form com parametros
        public frmEncerraPercurso(string recebeMotorista, string recebeVeiculo, string recebeId, string recebeIdMotorista, string recebeIdVeiculo, string recebeKmInicio )
        {
            dto = new DTOPercurso();
            InitializeComponent();
            //txt's recebendo os parametros do formPrincipal
            txtRecebeMotorista.Text = recebeMotorista;
            txtRecebeVeiculo.Text = recebeVeiculo;
            txtRegistro.Text = recebeId;
            txtRecebeKm.Text = recebeKmInicio;
            veiculo = Convert.ToInt32(recebeIdVeiculo);
            motorista = Convert.ToInt32(recebeIdMotorista);
            km_inicio = Convert.ToInt32(recebeKmInicio);
        }
        //evento load do form
        private void frmEncerraPercurso_Load(object sender, EventArgs e)
        {
            ListaCombustivel();
            dalPercurso.SelecionaTodosPercursos();
        }
        //listando os combustiveis do BD no cmbCombustivel
        private void ListaCombustivel()
        {
            dto = new DTOPercurso();
            dalPercurso = new PercursoAcessaDados();
            cmbCombustivel.DataSource = dalPercurso.SelecionaTodosCombustiveis();
            cmbCombustivel.DisplayMember = "nome_combustivel";
            cmbCombustivel.ValueMember = "id_combustivel";

            cmbCombustivel.SelectedIndex = -1;
        }
        //evento para mostrar a hora exata no form
        private void tmrChegada_Tick(object sender, EventArgs e)
        {
            lblDataChegada.Text = DateTime.Now.ToShortDateString();
            lblHoraChegada.Text = DateTime.Now.ToShortTimeString();
        }
        //limpar campos do form
        private void Limpar()
        {
            txtLitros.Text = String.Empty;
            txtRecebeKm.Text = String.Empty;
            txtRecebeMotorista.Text = String.Empty;
            txtRecebeVeiculo.Text = String.Empty;
            txtRegistro.Text = String.Empty;

            cmbCombustivel.Text = String.Empty;
        }
        //habilita o groupBox do form
        private void rbSim_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSim.Checked)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;

            }
        }
        //voltar ao menu anterior
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            fecharPagina();
        }
        //evento salvar do form
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaDados())
            {
                dalPercurso = new PercursoAcessaDados();
                dto = new DTOPercurso();
                dto.Motorista = motorista;
                dto.Veiculo = veiculo;
                dto.Km_veiculo = Convert.ToInt32(txtRecebeKm.Text.Trim());
                dto.Hora_final = lblHoraChegada.Text.Trim();
                dto.Periodo_final = lblDataChegada.Text.Trim();
                dto.Id_trajeto = Convert.ToInt32(txtRegistro.Text);
                dto.Ativo = 0; //desabilitando percurso do dgvPrincipal
                dto.Veiculo_Ativo = 1;//habilitando veiculo no cmbVeiculo do formPercurso
                dto.Motorista_Ativo = 1;//habilitando motorista no cmbMotorista do formPercurso

                if (km_inicio < dto.Km_veiculo)
                {
                    //seleção do rb
                    if (rbSim.Checked)
                    {
                        dto.Abastecido = "Sim";
                        dto.Litros_abastecido = Convert.ToDecimal(txtLitros.Text);
                        dto.Combustivel_abastecimento = Convert.ToInt32(cmbCombustivel.SelectedValue);
                    }
                    else
                    {
                        dto.Abastecido = "Não";
                        dto.Litros_abastecido = 0;
                        dto.Combustivel_abastecimento = 4;
                    }
                    //se houver um registro no campo txtRegistro
                    if (txtRegistro.Text != "0")
                    {
                        //encerra o percurso
                        dalPercurso.AtualizaFimTrajeto(dto);
                        MessageBox.Show("Trajeto FINALIZADO com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpar();
                        fecharPagina();
                    }
                }
                else
                {
                    MessageBox.Show("KM final é menor que o KM inicial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //fehcar e voltar ao menu anterior
        private void fecharPagina()
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //validação dos campos
        private Boolean ValidaDados()
        {
            if (txtRecebeKm.Text == string.Empty)
            {
                MessageBox.Show("Por favor, preencher o campo KM", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRecebeKm.Focus();
                return false;
            }
            if (groupBox1.Enabled == true)
            {
                if (txtLitros.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, preencher o campo LITROS", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLitros.Focus();
                    return false;
                }
                if (cmbCombustivel.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, verificar o campo COMBUSTÍVEL", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCombustivel.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}
