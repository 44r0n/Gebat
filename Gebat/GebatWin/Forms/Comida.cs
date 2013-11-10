using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            comboType.Refrescar(new ENType("").ReadAll());
            listaFoodIN.Refrescar(new ENFoodIN().ReadAll());
            comboFoodSalida.Refrescar(new ENFood("").ReadAll());
            comboFood.Refrescar(new ENFood("").ReadAll());
            listaFoodOut.Refrescar(new ENFoodOut().ReadAll());
            listaFood.Refrescar(new ENFood("").ReadAll());
            this.WindowState = FormWindowState.Maximized;
        }

        private void LoadComponents()
        {
            comboFood.Enabled = false;
            comboType.Refrescar(new ENType("").ReadAll());
            listaFoodIN.Refrescar(new ENFoodIN().ReadAll());
            comboFoodSalida.Refrescar(new ENFood("").ReadAll());
            comboFood.Refrescar(new ENFood("").ReadAll());
            listaFoodOut.Refrescar(new ENFoodOut().ReadAll());
            listaFood.Refrescar(new ENFood("").ReadAll());
        }

        #region//Control Radio

        private void radioButtonNuevo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNuevo.Checked)
            {
                textBoxNuevo.Enabled = true;
                comboFood.Enabled = false;
            }
            else
            {
                if (radioButtonExistente.Checked)
                {
                    textBoxNuevo.Enabled = false;
                    comboFood.Enabled = true;
                }
            }
        }

        private void radioButtonExistente_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonExistente.Enabled)
            {
                comboFood.Enabled = true;
                textBoxNuevo.Enabled = false;
            }
            else
            {
                if (radioButtonNuevo.Enabled)
                {
                    comboFood.Enabled = false;
                    textBoxNuevo.Enabled = true;
                }
            }
        }

        #endregion

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (radioButtonNuevo.Checked)
            {
                ENFood newfood = new ENFood(textBoxNuevo.Text, (ENType)comboType.Selected);
                newfood.Save();
                /*ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)newfood.MyType.Id[0]);
                foodin.Save();*/
                newfood.Add((int)numericUpDown.Value, dateTimePicker.Value);
                comboFood.Add(newfood);
                LoadComponents();
            }
            if (radioButtonExistente.Checked)
            {
                ENFood selected = (ENFood)comboFood.Selected;
                /*ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)selected.MyType.Id[0]);
                foodin.Save();*/
                selected.Add((int)numericUpDown.Value, dateTimePicker.Value);
                LoadComponents();
            }
        }

        private void comboFoodSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownSalida.Maximum = ((ENFood)comboFoodSalida.Selected).Quantity;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listaFood.Filter(textBoxSearch.Text);
            listaFoodIN.Filter(textBoxSearch.Text);
            listaFoodOut.Filter(textBoxSearch.Text);
        }

        private void buttonSalida_Click(object sender, EventArgs e)
        {
            ENFood salida = (ENFood)comboFoodSalida.Selected;
            salida.Remove((int)numericUpDownSalida.Value, dateTimePickerSalida.Value);
            LoadComponents();
        }
    }
}
