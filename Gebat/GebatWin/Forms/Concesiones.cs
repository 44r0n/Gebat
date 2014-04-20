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
using GebatEN.Enums;

namespace GebatWin.Forms
{
    public partial class Concesiones : Form
    {
        private EBPersonalDosier dossier;
        public Concesiones(EBPersonalDosier dossier)
        {
            InitializeComponent();
            this.dossier = dossier;
        }

        private void Fresco_Load(object sender, EventArgs e)
        {
            initValues();
        }

        private void initValues()
        {
            DateTime today = DateTime.Today;
            dateTimePickerBegin.Value = today;
            today.AddMonths(3);
            dateTimePickerFinish.Value = today;
            setListaConcesiones();
            textBoxObservations.Text = "";
            comboBoxState.SelectedItem = comboBoxState.Items[0];
            checkBoxFega_CheckedChanged(this, null);
        }

        private void setListaConcesiones()
        {
            listaConcesiones1.Clean();
            foreach (AEBConcession con in dossier.Concessions)
            {
                listaConcesiones1.Add((AEB)con);
            }
            listaConcesiones1.Show();
        }

        private void addFresco()
        {
            EBFresco fresco = new EBFresco();
            fresco.BeginDate = dateTimePickerBegin.Value;
            if (dateTimePickerFinish.Enabled)
            {
                fresco.FinishDate = dateTimePickerFinish.Value;
            }
            fresco.Notes = textBoxObservations.Text;
            dossier.AddConcession(fresco);
        }

        private void addFega()
        {
            EBFega fega = new EBFega();
            fega.BeginDate = dateTimePickerBegin.Value;
            if (dateTimePickerFinish.Enabled)
            {
                fega.FinishDate = dateTimePickerFinish.Value;
            }
            fega.Notes = textBoxObservations.Text;
            switch (comboBoxState.SelectedIndex)
            {
                case 0:
                    fega.State = FegaStates.Awaiting;
                    break;
                case 1:
                    fega.State = FegaStates.Suspended;
                    break;
                case 2:
                    fega.State = FegaStates.Aproved;
                    break;
            }
            dossier.AddConcession(fega);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxFega.Checked)
            {
                addFega();
            }
            else
            {
                addFresco();
            }
            initValues();
        }

        private void checkBoxFinishDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerFinish.Enabled = checkBoxFinishDate.Checked;
        }

        private void checkBoxFega_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxState.Enabled = checkBoxFega.Checked;
        }
    }
}
