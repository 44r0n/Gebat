using System;
using System.Windows.Forms;
using GebatEN.Classes;
using GebatEN.Enums;

namespace GebatWin.Forms
{
    public partial class TBC : Form
    {
        public TBC()
        {
            InitializeComponent();
        }

        private void TBC_Load(object sender, EventArgs e)
        {
            listaTBC.Refrescar(new ENTBC().ReadAll());
            comboDelito.Refrescar(new ENDelito().ReadAll());
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            ENTBC tbc;
            if(radioButtonHombre.Checked)
            {
                tbc = new ENTBC(textBoxDNI.Text, textBoxEjecutoria.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNac.Value, sexo.Masculino, textBoxJuzgado.Text, dateTimePickerInicio.Value, dateTimePickerFin.Value, (ENDelito)comboDelito.Selected);
            }
            else
            {
                tbc = new ENTBC(textBoxDNI.Text, textBoxEjecutoria.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNac.Value ,sexo.Femenino, textBoxJuzgado.Text, dateTimePickerInicio.Value, dateTimePickerFin.Value, (ENDelito)comboDelito.Selected);
            }
            tbc.NumJornadas = (int)numericUpDownJornadas.Value;
            tbc.Horario[DayOfWeek.Monday] = checkBoxLunes.Checked;
            tbc.Horario[DayOfWeek.Tuesday] = checkBoxMartes.Checked;
            tbc.Horario[DayOfWeek.Wednesday] = checkBoxMiercoles.Checked;
            tbc.Horario[DayOfWeek.Thursday] = checkBoxJueves.Checked;
            tbc.Horario[DayOfWeek.Friday] = checkBoxViernes.Checked;
            tbc.Horario[DayOfWeek.Saturday] = checkBoxSabado.Checked;
            tbc.Horario[DayOfWeek.Sunday] = checkBoxDomingo.Checked;
            tbc.Save();
            listaTBC.Add(tbc);
            textBoxDNI.Text = "";
            textBoxEjecutoria.Text = "";
            textBoxNombre.Text = "";
            textBoxApellidos.Text = "";
            textBoxJuzgado.Text = "";
            checkBoxLunes.Checked = false;
            checkBoxMartes.Checked = false;
            checkBoxMiercoles.Checked = false;
            checkBoxJueves.Checked = false;
            checkBoxViernes.Checked = false;
            checkBoxSabado.Checked = false;
            checkBoxDomingo.Checked = false;
            dateTimePickerFin.Value = DateTime.Today;
            dateTimePickerInicio.Value = DateTime.Today;
            textBoxDNI.Focus();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listaTBC.Filter(textBoxSearch.Text);
        }

        private void listaTBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
            buttonInicio.Enabled = true;
            buttonFin.Enabled = true;
            buttonFirmas.Enabled = true;
            buttonAddTelf.Enabled = true;
            showTelfs((AENPersona)listaTBC.Selected);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            AEN tbc = listaTBC.Selected;
            tbc.Delete();
            listaTBC.Refrescar(new ENTBC().ReadAll());
            buttonDelete.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ENTBC tbc = (ENTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.FileName = "Firmas de " + tbc.Nombre + " " + tbc.DNI + ".pdf";
            savedialog.Title = "Generar Firmas...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.FirmasToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonInicio_Click(object sender, EventArgs e)
        {
            ENTBC tbc = (ENTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = "Inicio de " + tbc.Nombre + " " + tbc.DNI + ".pdf";
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.Title = "Generar Inicio...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.InicioSentenciaToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonFin_Click(object sender, EventArgs e)
        {
            ENTBC tbc = (ENTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = "Fin de de " + tbc.Nombre + " " + tbc.DNI + ".pdf";
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.Title = "Generar Fin...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.FinSentenciaToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonAddTelf_Click(object sender, EventArgs e)
        {
            AENPersona selected = (AENPersona)listaTBC.Selected;
            AddTelfs addTelfsForm = new AddTelfs(selected);
            addTelfsForm.ShowDialog();
            addTelfsForm.BringToFront();
            showTelfs(selected);
        }
    }
}
