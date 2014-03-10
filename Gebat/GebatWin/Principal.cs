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
            ADL.Password = contraseña;
            ADL.Connect("GebatDataConnectionString");
            
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            ADL.Disconnect();
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

        private void comidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Comida comida = new Forms.Comida();
            comida.MdiParent = this;
            comida.Show();
        }

        private void tBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.TBC tbc = new Forms.TBC();
            tbc.MdiParent = this;
            tbc.Show();
        }

        private void delitosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Delitos delitos = new Forms.Delitos();
            delitos.MdiParent = this;
            delitos.Show();
        }

        private void expedientesPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.ExpedientePersonal exp = new Forms.ExpedientePersonal();
            exp.MdiParent = this;
            exp.Show();
        }
    }
}
