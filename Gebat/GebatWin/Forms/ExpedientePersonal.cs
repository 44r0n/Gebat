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
        }

        private void ExpedientePersonal_Load(object sender, EventArgs e)
        {
            listaExpedientes.Refrescar(new EBPersonalDosier().ReadAll());
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            EBPersonalDosier exp = new EBPersonalDosier(textBoxObservaciones.Text);
            textBoxObservaciones.Text = "";
            exp.Save();
            listaExpedientes.Add(exp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AEB exp = listaExpedientes.Selected;
            exp.Delete();
            listaExpedientes.Refrescar(new EBPersonalDosier().ReadAll());
            buttonDelete.Enabled = false;
            buttonAddFamiliar.Enabled = false;
            buttonAddConcessions.Enabled = false;
        }

        private void listaExpedientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
            buttonAddFamiliar.Enabled = true;
            buttonAddConcessions.Enabled = true;
            listaFamiliares.Clean();
            listaConcesiones1.Clean();
            EBPersonalDosier exp = (EBPersonalDosier)listaExpedientes.Selected;
            if (exp != null)
            {
                mostrarFamiliares(exp);
                mostrarConcesiones(exp);
            }
        }

        private void mostrarFamiliares(EBPersonalDosier expediente)
        {
            List<EBFamiliar> fams = expediente.Familiars;
            if (fams != null)
            {
                foreach (EBFamiliar fam in fams)
                {
                    listaFamiliares.Add(fam);
                }
            }
            listaExpedientes.Show();
        }

        private void mostrarConcesiones(EBPersonalDosier expediente)
        {
            if (expediente.Concessions != null)
            {
                foreach (AEBConcession con in expediente.Concessions)
                {
                    listaConcesiones1.Add((AEB)con);
                }
            }
            listaConcesiones1.Show();
        }

        private void buttonAddFamiliar_Click(object sender, EventArgs e)
        {
            GestionFamiliares gestionfam = new GestionFamiliares((EBPersonalDosier)listaExpedientes.Selected);
            gestionfam.ShowDialog();
            gestionfam.BringToFront();
            buttonAddFamiliar.Enabled = false;
            buttonDelete.Enabled = false;
            buttonAddConcessions.Enabled = false;
            listaFamiliares.Clean();
            mostrarFamiliares((EBPersonalDosier)listaExpedientes.Selected);
            //listaExpedientes.Refrescar(new EBPersonalDosier().ReadAll());
        }

        private void buttonAddConcessions_Click(object sender, EventArgs e)
        {
            Fresco frescoform = new Fresco((EBPersonalDosier)listaExpedientes.Selected);
            frescoform.ShowDialog();
            frescoform.BringToFront();
            buttonAddFamiliar.Enabled = false;
            buttonDelete.Enabled = false;
            buttonAddConcessions.Enabled = false;
            listaConcesiones1.Clean();
            mostrarConcesiones((EBPersonalDosier)listaExpedientes.Selected);
        }
    }
}
