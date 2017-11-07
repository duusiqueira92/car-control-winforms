using DAL;
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
    public partial class frmLogin : Form
    {
        UsuarioAcessaDados usuario;
        Criptografia senhaCripto;
        public frmLogin()
        {
            InitializeComponent();
        }
        //evento para entrar no sistema
        private void btnLogin_Click(object sender, EventArgs e)
        {
            senhaCripto = new Criptografia();
            usuario = new UsuarioAcessaDados();
            DataTable dadosTabela = new DataTable();

            dadosTabela = usuario.Login(txtUsuario.Text, senhaCripto.Criptografar(txtSenha.Text));

            if (dadosTabela.Rows.Count == 0)
            {
                MessageBox.Show("Usuário ou senha inválidos!", "Entrada não permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
        }
        //evento sair do sistema
        private void brnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //evento para mudar o campo após pressionar o key.char(13)(Enter)
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtSenha.Focus();
            }
        }
        //evento para mudar o campo após pressionar o key.char(13)(Enter)
        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.Focus();
            }
        }
    }
}
