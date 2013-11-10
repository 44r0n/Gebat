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
            comboFood.Refrescar(new ENFood("").ReadAll());
            listaFood.Refrescar(new ENFood("").ReadAll());
            this.WindowState = FormWindowState.Maximized;
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
                ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)newfood.MyType.Id[0]);
                foodin.Save();
                comboFood.Add(newfood);
                listaFoodIN.Add(foodin);
            }
            if (radioButtonExistente.Checked)
            {
                ENFood selected = (ENFood)comboFood.Selected;
                ENFoodIN foodin = new ENFoodIN(dateTimePicker.Value, (int)numericUpDown.Value, (int)selected.MyType.Id[0]);
                foodin.Save();
                listaFoodIN.Add(foodin);
            }
        }
    }
}
