using System;
using System.Windows.Forms;
using GebatCAD.Classes;

namespace GebatWin
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            enableConnect();   
        }

        private void enableConnect()
        {
            if (textBoxUser.Text != "" && maskedTextBoxPassword.Text != "")
            {
                buttonConnect.Enabled = true;
            }
        }

        private void maskedTextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            enableConnect();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            AADL.Password = maskedTextBoxPassword.Text;
            if (AADL.AttemptConnection("GebatDataConnectionString"))
            {
                Principal prin = new Principal(maskedTextBoxPassword.Text);
                prin.Show();
                prin.BringToFront();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Imposible conectar.", "Aviso", MessageBoxButtons.OK);
            }
        }
    }
}
