using System;
using System.Windows.Forms;
using GebatEN.Classes;

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
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            ENTBC tbc = new ENTBC(textBoxDNI.Text, textBoxEjecutoria.Text, textBoxNombre.Text, textBoxApellidos.Text, textBoxJuzgado.Text, dateTimePickerInicio.Value, dateTimePickerFin.Value);
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
            savedialog.FileName = "Archivo PDF|*.pdf";
            savedialog.Title = "Generar Firmas...";
            savedialog.ShowDialog();

            MessageBox.Show(savedialog.FileName);
        }
    }
}
