using System;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWin.Forms
{
    public partial class Comida : Form
    {
        public Comida()
        {
            InitializeComponent();
        }

        private void Comida_Load(object sender, EventArgs e)
        {
            comboFood.Enabled = false;
            LoadComponents();
            this.WindowState = FormWindowState.Maximized;
        }

        private void LoadComponents()
        {            
            comboType.Refrescar(new EBType("").ReadAll());
            listaFoodIN.Refrescar(new EBFoodIN().ReadAll());
            comboFoodSalida.Refrescar(new EBFood("").ReadAll());
            comboFood.Refrescar(new EBFood("").ReadAll());
            listaFoodOut.Refrescar(new EBFoodOut().ReadAll());
            listaFood.Refrescar(new EBFood("").ReadAll());
        }

        #region//Control Radio

        private void radioButtonNuevo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNuevo.Checked)
            {
                textBoxNuevo.Enabled = true;
                comboFood.Enabled = false;
                comboType.Enabled = true;
            }
            else
            {
                if (radioButtonExistente.Checked)
                {
                    textBoxNuevo.Enabled = false;
                    comboFood.Enabled = true;
                    comboType.Enabled = false;
                }
            }
        }

        private void radioButtonExistente_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonExistente.Enabled)
            {
                comboFood.Enabled = true;
                textBoxNuevo.Enabled = false;
                comboType.Enabled = false;
            }
            else
            {
                if (radioButtonNuevo.Enabled)
                {
                    comboFood.Enabled = false;
                    textBoxNuevo.Enabled = true;
                    comboType.Enabled = true;
                }
            }
        }

        #endregion

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (radioButtonNuevo.Checked)
            {
                EBFood newfood = new EBFood(textBoxNuevo.Text, (EBType)comboType.Selected);
                newfood.Save();
                /*ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)newfood.MyType.Id[0]);
                foodin.Save();*/
                newfood.Add((int)numericUpDown.Value, dateTimePicker.Value);
                comboFood.Add(newfood);
                LoadComponents();
            }
            if (radioButtonExistente.Checked)
            {
                EBFood selected = (EBFood)comboFood.Selected;
                /*ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)selected.MyType.Id[0]);
                foodin.Save();*/
                selected.Add((int)numericUpDown.Value, dateTimePicker.Value);
                LoadComponents();
            }
            comboFood.SelectedIndex = -1;
            comboFood.Text = "";
            radioButtonNuevo.Checked = true;
            radioButtonExistente.Checked = false;
            textBoxNuevo.Text = "";
            numericUpDown.Value = 0;
            comboType.SelectedIndex = -1;
            comboType.Text = "";
            dateTimePicker.Value = DateTime.Today;
        }

        private void comboFoodSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownSalida.Maximum = ((EBFood)comboFoodSalida.Selected).Quantity;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listaFood.Filter(textBoxSearch.Text);
            listaFoodIN.Filter(textBoxSearch.Text);
            listaFoodOut.Filter(textBoxSearch.Text);
        }

        private void buttonSalida_Click(object sender, EventArgs e)
        {
            EBFood salida = (EBFood)comboFoodSalida.Selected;
            salida.Remove((int)numericUpDownSalida.Value, dateTimePickerSalida.Value);
            LoadComponents();
            comboFoodSalida.SelectedIndex = -1;
            comboFoodSalida.Text = "";
            numericUpDownSalida.Value = 0;
        }
    }
}
