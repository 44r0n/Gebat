using System;
using System.Windows.Forms;

namespace GebatWin.Forms
{
    public partial class Cantidades : Form
    {
        public Cantidades()
        {
            InitializeComponent();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            /*EBType nuevo = new EBType(textBoxNombreTipo.Text);
            nuevo.Save();
            listaTypes1.Add(nuevo);
            textBoxNombreTipo.Text = "";
            textBoxNombreTipo.Focus();*/
        }

        private void Cantidades_Load(object sender, EventArgs e)
        {
            //listaTypes1.Refrescar(new EBType("").ReadAll());
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listaTypes1.Filter(textBoxSearch.Text);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
           /* EBType tipo = (EBType)listaTypes1.Selected;
            tipo.Delete();
            listaTypes1.Refrescar(tipo.ReadAll());*/
        }
    }
}