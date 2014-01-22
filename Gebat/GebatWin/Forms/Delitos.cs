using System;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWin.Forms
{
    public partial class Delitos : Form
    {
        public Delitos()
        {
            InitializeComponent();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            ENDelito nuevo = new ENDelito(textBoxNombre.Text);
            nuevo.Save();
            listaDelito1.Add(nuevo);
            textBoxNombre.Text = "";
            textBoxNombre.Focus();
        }

        private void Delitos_Load(object sender, EventArgs e)
        {
            listaDelito1.Refrescar(new ENDelito().ReadAll());
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listaDelito1.Filter(textBoxSearch.Text);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ENDelito delito = (ENDelito)listaDelito1.Selected;
            delito.Delete();
            listaDelito1.Refrescar(delito.ReadAll());
        }
    }
}
