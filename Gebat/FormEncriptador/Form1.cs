using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cripto.Util;

namespace FormEncriptador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private Cipher cipher = new Cipher("contra1", "salt1", "contras2", "salte2");

        private void button1_Click(object sender, EventArgs e)
        {
            /*string ciphered = cipher.Encrypt(textBox1.Text);
            Clipboard.SetDataObject(ciphered);
            MessageBox.Show("Copiado al portapapeles", "Copiado", MessageBoxButtons.OK);*/
        }
    }
}
