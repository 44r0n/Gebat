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
            listaTBC.Refrescar(new EBTBC().ReadAll());
            comboDelito.Refrescar(new EBCrime().ReadAll());
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            EBTBC tbc;
            if(radioButtonHombre.Checked)
            {
                tbc = new EBTBC(textBoxDNI.Text, textBoxEjecutoria.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNac.Value, MyGender.Male, textBoxJuzgado.Text, dateTimePickerInicio.Value, dateTimePickerFin.Value, dateTimePickerBeginHour.Value.TimeOfDay, dateTimePickerFinishHour.Value.TimeOfDay,(EBCrime)comboDelito.Selected);
            }
            else
            {
                tbc = new EBTBC(textBoxDNI.Text, textBoxEjecutoria.Text, textBoxNombre.Text, textBoxApellidos.Text, dateTimePickerFechaNac.Value, MyGender.Female, textBoxJuzgado.Text, dateTimePickerInicio.Value, dateTimePickerFin.Value, dateTimePickerBeginHour.Value.TimeOfDay, dateTimePickerFinishHour.Value.TimeOfDay, (EBCrime)comboDelito.Selected);
            }
            tbc.NumJourney = (int)numericUpDownJornadas.Value;
            tbc.Timetable[DayOfWeek.Monday] = checkBoxLunes.Checked;
            tbc.Timetable[DayOfWeek.Tuesday] = checkBoxMartes.Checked;
            tbc.Timetable[DayOfWeek.Wednesday] = checkBoxMiercoles.Checked;
            tbc.Timetable[DayOfWeek.Thursday] = checkBoxJueves.Checked;
            tbc.Timetable[DayOfWeek.Friday] = checkBoxViernes.Checked;
            tbc.Timetable[DayOfWeek.Saturday] = checkBoxSabado.Checked;
            tbc.Timetable[DayOfWeek.Sunday] = checkBoxDomingo.Checked;
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
            showTelfs((AEBPerson)listaTBC.Selected);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            AEB tbc = listaTBC.Selected;
            tbc.Delete();
            listaTBC.Refrescar(new EBTBC().ReadAll());
            buttonDelete.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EBTBC tbc = (EBTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.FileName = "Firmas de " + tbc.Name + " " + tbc.DNI + ".pdf";
            savedialog.Title = "Generar Firmas...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.SignaturesToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonInicio_Click(object sender, EventArgs e)
        {
            EBTBC tbc = (EBTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = "Inicio de " + tbc.Name + " " + tbc.DNI + ".pdf";
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.Title = "Generar Inicio...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.BeginSentenceToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonFin_Click(object sender, EventArgs e)
        {
            EBTBC tbc = (EBTBC)listaTBC.Selected;
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = "Fin de de " + tbc.Name + " " + tbc.DNI + ".pdf";
            savedialog.Filter = "Archivo PDF|*.pdf";
            savedialog.Title = "Generar Fin...";
            savedialog.ShowDialog();

            if (savedialog.FileName != "")
            {
                tbc.FinishSentenceToPDF(savedialog.FileName);
                System.Diagnostics.Process.Start(savedialog.FileName);
            }
        }

        private void buttonAddTelf_Click(object sender, EventArgs e)
        {
            AEBPerson selected = (AEBPerson)listaTBC.Selected;
            AddTelfs addTelfsForm = new AddTelfs(selected);
            addTelfsForm.ShowDialog();
            addTelfsForm.BringToFront();
            showTelfs(selected);
        }
    }
}
