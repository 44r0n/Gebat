using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GebatEN.Classes;

namespace GebatWin.Forms
{
    public partial class ExpedientePersonal : Form
    {
        public ExpedientePersonal()
        {
            InitializeComponent();
            numericUpDownIngresos.Maximum = decimal.MaxValue;
        }

        private void ExpedientePersonal_Load(object sender, EventArgs e)
        {
            listaExpedientes.Refrescar(new ENExpedientePersonal().ReadAll());
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            ENExpedientePersonal exp = new ENExpedientePersonal((int)numericUpDownIngresos.Value, textBoxObservaciones.Text);
            numericUpDownIngresos.Value = 0;
            textBoxObservaciones.Text = "";
            exp.Save();
            listaExpedientes.Add(exp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AEN exp = listaExpedientes.Selected;
            exp.Delete();
            listaExpedientes.Refrescar(new ENExpedientePersonal().ReadAll());
            buttonDelete.Enabled = false;
            buttonAddFamiliar.Enabled = false;
        }

        private void listaExpedientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
            buttonAddFamiliar.Enabled = true;
            listaFamiliares.Clean();
            ENExpedientePersonal exp = (ENExpedientePersonal)listaExpedientes.Selected;
            if (exp != null)
            {
                mostrarFamiliares(exp);   
            }
        }

        private void mostrarFamiliares(ENExpedientePersonal expediente)
        {
            List<ENFamiliar> fams = expediente.Familiares;
            if (fams != null)
            {
                foreach (ENFamiliar fam in fams)
                {
                    listaFamiliares.Add(fam);
                }
            }
        }

        private void buttonAddFamiliar_Click(object sender, EventArgs e)
        {
            GestionFamiliares gestionfam = new GestionFamiliares((ENExpedientePersonal)listaExpedientes.Selected);
            gestionfam.ShowDialog();
            gestionfam.BringToFront();
            buttonAddFamiliar.Enabled = false;
            buttonDelete.Enabled = false;
        }
    }
}
