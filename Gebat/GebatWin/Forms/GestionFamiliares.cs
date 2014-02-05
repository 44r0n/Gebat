using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GebatEN.Classes;
using GebatEN.Enums;

namespace GebatWin.Forms
{
    public partial class GestionFamiliares : Form
    {
        private ENExpedientePersonal expediente;

        public GestionFamiliares(ENExpedientePersonal expediente)
        {
            InitializeComponent();
            this.expediente = expediente;
            mostrarFamiliares();
        }

        private void mostrarFamiliares()
        {
            listaFamiliares.Clean();
            foreach (ENFamiliar fam in expediente.Familiares)
            {
                listaFamiliares.Add(fam);
            }
        }

        private void buttonAddFamiliar_Click(object sender, EventArgs e)
        {
            if (radioButtonHombre.Checked)
            {
                expediente.AddFamiliar(new ENFamiliar(textBoxDNI.Text,textBoxNombre.Text,textBoxApellidos.Text,dateTimePickerFechaNacimiento.Value,sexo.Masculino));
            }

            if (radioButtonMujer.Checked)
            {
                expediente.AddFamiliar(new ENFamiliar(textBoxDNI.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNacimiento.Value, sexo.Femenino));
            }

            mostrarFamiliares();
        }

        private void showTelfs(AENPersona persona)
        {
            listViewTelfs.Items.Clear();
            if (persona != null)
            {
                int i = 0;
                foreach (string telf in persona.Telefonos)
                {
                    ListViewItem item = new ListViewItem(i.ToString(), 0);
                    item.SubItems.Add(telf);
                    listViewTelfs.Items.Add(item);
                    i++;
                }
            }
        }

        private void listaFamiliares_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddTelf.Enabled = true;
            showTelfs((AENPersona)listaFamiliares.Selected);
        }

        private void buttonAddTelf_Click(object sender, EventArgs e)
        {
            AENPersona selected = (AENPersona)listaFamiliares.Selected;
            AddTelfs addTelfsForm = new AddTelfs(selected);
            addTelfsForm.ShowDialog();
            addTelfsForm.BringToFront();
            showTelfs(selected);
        }
    }
}
