using DAL;
using DTO;
using MySql.Data.MySqlClient;
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
    public partial class frmPercurso : Form
    {
        //CriaBancoAcessoDados db;
        PercursoAcessaDados dalPercurso;
        MotoristaAcessaDados novoMotorista;
        DTOPercurso dto;
        FuncionarioAcessaDados dal;

        public frmPercurso()
        {
            InitializeComponent();
        }

        //evento iniciado ao carregar o formulario
        private void frmPercurso_Load(object sender, EventArgs e)
        {
            lblDataSaida.Text = DateTime.Now.ToShortDateString();

            //metodos para carregar os combobox
            //ListaCombustivel();
            ListaMotoristas();
            ListaVeiculo();

            //metodo para popular o dgv
            ListarPercursoAtivo();

            //formatação de form
            Estilo();
            Limpar();
        }
        //lista motorista no cmbMotorista
        private void ListaMotoristas()
        {
            novoMotorista = new MotoristaAcessaDados();

            cmbMotorista.DataSource = novoMotorista.SelecionaTodosMotoristas();
            cmbMotorista.DisplayMember = "nome";
            cmbMotorista.ValueMember = "id_motorista";

            cmbMotorista.SelectedIndex = -1;
        }
        //lista veiculo no cmbVeiculo
        private void ListaVeiculo()
        {
            dto = new DTOPercurso();
            dalPercurso = new PercursoAcessaDados();
            cmbVeiculo.DataSource = dalPercurso.SelecionaTodosVeiculos();
            cmbVeiculo.DisplayMember = "modelo";
            cmbVeiculo.ValueMember = "id_veiculo";
            
            dto.Id_veiculo = Convert.ToInt32(cmbVeiculo.SelectedValue);
            cmbVeiculo.SelectedIndex = -1;
        }
        //evento click do botão salvar
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaDados())
            {
                dal = new FuncionarioAcessaDados();
                dto = new DTOPercurso();
                dto.Veiculo = Convert.ToInt32(cmbVeiculo.SelectedValue);
                dto.Motorista = Convert.ToInt32(cmbMotorista.SelectedValue);
                dto.Km_veiculo = Convert.ToInt32(txtKm.Text);
                dto.Periodo_inicial = lblDataSaida.Text;
                dto.Hora_inicial = lblHoraSaida.Text;
                dto.Veiculo_Ativo = 0;
                dto.Motorista_Ativo = 0;
                dto.Id_trajeto = Convert.ToInt32(txtRegistro.Text);
            

           
                //dto.Periodo_inicial = 
                if (txtRegistro.Text == "0")
                {
                    dalPercurso.CadastroPercurso(dto);
                   dalPercurso.AtualizaVeiculo(dto);
                   dalPercurso.AtualizaMotorista(dto);
                    MessageBox.Show("Trajeto CADASTRADO com sucesso!", "Inserido com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //atualiza funcionario existente
                    dto.Id_trajeto = int.Parse(txtRegistro.Text);
                    dalPercurso.AtualizaTrajeto(dto);
                    dalPercurso.AtualizaVeiculo(dto);
                    dalPercurso.AtualizaMotorista(dto);
                    MessageBox.Show("Trajeto ATUALIZADO com sucesso!", "Atualizado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ListarPercursoAtivo();
                ListaVeiculo();
                ListaMotoristas();
                Limpar();
            }
        }
        //metodo para preencher o data grid view
        private void ListarPercursoAtivo()
        {
            dal = new FuncionarioAcessaDados();
            dtgPercursos.DataSource = dalPercurso.SelecionaTodosPercursos();
            Estilo();
        }
        //metodo para dar estilo ao data grid view
        private void Estilo()
        {
            for (int i = 0; i < dtgPercursos.Rows.Count; i += 2)
            {
                dtgPercursos.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //metodo para limpar o formulario
        private void Limpar()
        {
            txtRegistro.Text = "0";
            //txtLitros.Clear();
            txtKm.Clear();
            //cmbCombustivel.SelectedIndex = -1;
            cmbMotorista.SelectedIndex = -1;
            cmbVeiculo.SelectedIndex = -1;
        }
        //evento click do data grid view
        private void dtgPercursos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //exclui a linha selecionada do banco de dados
                if (dtgPercursos.Columns[e.ColumnIndex].Name == "btnDeletar" && MessageBox.Show("Deseja realmente excluir?", "Deseja excluir?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dalPercurso.ExcluiTrajeto(Convert.ToInt32(dtgPercursos.Rows[e.RowIndex].Cells["id_trajeto"].Value));
                    dto.Motorista_Ativo = 1; //ativa o motorista para ser mostrado no cmbMotorista
                    dto.Veiculo_Ativo = 1;//ativa o veiculo para ser mostrado no cmbVeiculo
                    dto.Veiculo = Convert.ToInt32(dtgPercursos.Rows[e.RowIndex].Cells["id_veiculo"].Value);
                    dto.Motorista = Convert.ToInt32(dtgPercursos.Rows[e.RowIndex].Cells["id_motorista"].Value);
                    dalPercurso.AtualizaMotorista(dto);
                    dalPercurso.AtualizaVeiculo(dto);
                    MessageBox.Show("Percurso EXCLUÍDO com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ListarPercursoAtivo();
                ListaMotoristas();
                ListaVeiculo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar o PERCURSO! " + ex.Message, "Erro 0002");
            }
        }
        //retorna o km do veiculo selecionado
        private void cmbVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVeiculo.SelectedIndex != -1)
            {
                DataRowView drw = ((DataRowView)cmbVeiculo.SelectedItem);
                txtKm.Text = drw["km_veiculo"].ToString();
            }
            else
            {
                txtKm.Text = "";
            }
        }
        //evento para retornar a hora exata no form
        private void tmrHora_Tick(object sender, EventArgs e)
        {
            lblHoraSaida.Text = DateTime.Now.ToShortTimeString();
        }
        //voltar ao menu anterior
        private void tsVoltar_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //validação dos campos
        private Boolean validaDados()
        {
            if (cmbVeiculo.Text == string.Empty)
            {
                MessageBox.Show("Verificar o campo VEÍCULO", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbVeiculo.Focus();
                return false;
            }
            if (cmbMotorista.Text == string.Empty)
            {
                MessageBox.Show("Verificar o campo MOTORISTA", "Erro 0000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMotorista.Focus();
                return false;
            }            
            return true;
        }
    }
}
