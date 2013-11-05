using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GebatCAD.Classes;

namespace GebatWin
{
    public partial class Principal : Form
    {
        public Principal(string contraseña)
        {
            InitializeComponent();
            ACAD.Password = contraseña;
            ACAD.Connect("GebatDataConnectionString");
            
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            ACAD.Disconnect();
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cantidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Cantidades cants = new Forms.Cantidades();
            cants.ShowDialog();
        }
    }
}
