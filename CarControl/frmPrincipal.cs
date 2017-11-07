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
    public partial class frmPrincipal : Form
    {
        PercursoAcessaDados dalPercurso;
        public frmPrincipal()
        {
            InitializeComponent();
        }
        //carrega os metodos no form
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            dalPercurso = new PercursoAcessaDados();
            dgvTransito.DataSource = dalPercurso.SelecionaTodosPercursos();
            Estilo();
        }
        //abre o form funcionarios
        private void tsFuncionarios_Click(object sender, EventArgs e)
        {
            frmFuncionarios frm = new frmFuncionarios();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //abre o form veiculos
        private void tsVeiculos_Click(object sender, EventArgs e)
        {
            frmVeiculos frm = new frmVeiculos();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //abre o form motoristas
        private void tsMotoristas_Click(object sender, EventArgs e)
        {
            frmMotorista frm = new frmMotorista();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //abre o form percurso
        private void tsPercurso_Click(object sender, EventArgs e)
        {
            frmPercurso frm = new frmPercurso();
            this.Hide();
            frm.ShowDialog();
            this.Close();
            
        }
        //volta a tela de login
        private void tsLogout_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //intercala uma linha colorida e uma branca no dgv
        private void Estilo()
        {
            for (int i = 0; i < dgvTransito.Rows.Count; i += 2)
            {
                dgvTransito.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        //evento click do dgv
        public void dgvTransito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTransito.Columns[e.ColumnIndex].Name == "btnEncerra")
            {
                string enviaMotorista = dgvTransito.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                string enviaVeiculo = dgvTransito.Rows[e.RowIndex].Cells["modelo"].Value.ToString();
                string enviaId = dgvTransito.Rows[e.RowIndex].Cells["id_trajeto"].Value.ToString();
                string enviaIdMotorista = dgvTransito.Rows[e.RowIndex].Cells["id_motorista"].Value.ToString();
                string enviaIdVeiculo = dgvTransito.Rows[e.RowIndex].Cells["id_veiculo"].Value.ToString();
                string enviaKmInicio = dgvTransito.Rows[e.RowIndex].Cells["km_inicio"].Value.ToString();

                frmEncerraPercurso frm = new frmEncerraPercurso(enviaMotorista, enviaVeiculo, enviaId, enviaIdMotorista, enviaIdVeiculo, enviaKmInicio);
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
        }
        //abre o form cadastro_login
        private void tsCadLogin_Click(object sender, EventArgs e)
        {
            frmCadastroLogin frm = new frmCadastroLogin();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
        //abre o form relatorio
        private void tsRelatorio_Click(object sender, EventArgs e)
        {
            frmRelatorio frm = new frmRelatorio();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
