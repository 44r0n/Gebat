using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
