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

        private void DisableButtons()
        {
            buttonDelete.Enabled = false;
            buttonAddFamiliar.Enabled = false;
            buttonAddConcessions.Enabled = false;
            buttonChangeState.Enabled = false;
            buttonDeleteFamiliar.Enabled = false;
            buttonDelConcesion.Enabled = false;
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
            DisableButtons();
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

        private void listaFamiliares_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteFamiliar.Enabled = true;
        }

        private void listaConcesiones1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AEBConcession con = (AEBConcession)listaConcesiones1.Selected;
            if (con != null)
            {
                if (con.GetType() == typeof(EBFega))
                {
                    buttonChangeState.Enabled = true;
                }
                else
                {
                    buttonChangeState.Enabled = false;
                }
                buttonDelConcesion.Enabled = true;
            }
        }

        private void buttonDeleteFamiliar_Click(object sender, EventArgs e)
        {
            EBFamiliar fam = (EBFamiliar)listaFamiliares.Selected;
            if (fam != null)
            {
                fam.Delete();
                listaFamiliares.Refrescar(fam.ReadAll());
            }
            buttonDeleteFamiliar.Enabled = false;
        }

        private void buttonDelConcesion_Click(object sender, EventArgs e)
        {
            AEBConcession con = (AEBConcession)listaConcesiones1.Selected;
            if (con != null)
            {
                con.Delete();
                EBPersonalDosier exp = (EBPersonalDosier)listaExpedientes.Selected;
                if (exp != null)
                {
                    mostrarConcesiones(exp);
                }
            }
            buttonDelConcesion.Enabled = false;
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
                listaConcesiones1.Clean();
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
            DisableButtons();
            listaFamiliares.Clean();
            mostrarFamiliares((EBPersonalDosier)listaExpedientes.Selected);
            //listaExpedientes.Refrescar(new EBPersonalDosier().ReadAll());
        }

        private void buttonAddConcessions_Click(object sender, EventArgs e)
        {
            Concesiones frescoform = new Concesiones((EBPersonalDosier)listaExpedientes.Selected);
            frescoform.ShowDialog();
            frescoform.BringToFront();
            DisableButtons();
            listaConcesiones1.Clean();
            mostrarConcesiones((EBPersonalDosier)listaExpedientes.Selected);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeState changeform = new ChangeState((EBFega)listaConcesiones1.Selected);
            changeform.ShowDialog();
            changeform.BringToFront();
            DisableButtons();
            listaConcesiones1.Clean();
            mostrarConcesiones((EBPersonalDosier)listaExpedientes.Selected);
        }
    }
}
