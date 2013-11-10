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
            tbc.Save();
            listaTBC.Add(tbc);
            textBoxDNI.Text = "";
            textBoxEjecutoria.Text = "";
            textBoxNombre.Text = "";
            textBoxApellidos.Text = "";
            textBoxJuzgado.Text = "";
            dateTimePickerFin.Value = DateTime.Today;
            dateTimePickerInicio.Value = DateTime.Today;
            textBoxDNI.Focus();
        }
    }
}
