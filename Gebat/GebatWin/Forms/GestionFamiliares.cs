using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GebatEN.Classes;
using GebatEN.Enums;

namespace GebatWin.Forms
{
    public partial class GestionFamiliares : Form
    {
        private EBPersonalDosier expediente;

        public GestionFamiliares(EBPersonalDosier expediente)
        {
            InitializeComponent();
            this.expediente = expediente;
            mostrarFamiliares();
        }

        private void mostrarFamiliares()
        {
            listaFamiliares.Clean();
            foreach (EBFamiliar fam in expediente.Familiars)
            {
                listaFamiliares.Add(fam);
            }
        }

        private void buttonAddFamiliar_Click(object sender, EventArgs e)
        {
            if (radioButtonHombre.Checked)
            {
                expediente.AddFamiliar(new EBFamiliar(textBoxDNI.Text,textBoxNombre.Text,textBoxApellidos.Text,dateTimePickerFechaNacimiento.Value,MyGender.Male));
            }

            if (radioButtonMujer.Checked)
            {
                expediente.AddFamiliar(new EBFamiliar(textBoxDNI.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNacimiento.Value, MyGender.Female));
            }

            mostrarFamiliares();
        }

        private void showTelfs(AEBPerson persona)
        {
            listViewTelfs.Items.Clear();
            if (persona != null)
            {
                int i = 0;
                foreach (string telf in persona.Phones)
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
            showTelfs((AEBPerson)listaFamiliares.Selected);
        }

        private void buttonAddTelf_Click(object sender, EventArgs e)
        {
            AEBPerson selected = (AEBPerson)listaFamiliares.Selected;
            AddTelfs addTelfsForm = new AddTelfs(selected);
            addTelfsForm.ShowDialog();
            addTelfsForm.BringToFront();
            showTelfs(selected);
        }
    }
}
