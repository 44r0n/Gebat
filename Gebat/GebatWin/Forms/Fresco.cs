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
    public partial class Fresco : Form
    {
        private EBPersonalDosier dossier;
        public Fresco(EBPersonalDosier dossier)
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

        private void button1_Click(object sender, EventArgs e)
        {
            EBFresco fresco = new EBFresco();
            fresco.BeginDate = dateTimePickerBegin.Value;
            if (dateTimePickerFinish.Enabled)
            {
                fresco.FinishDate = dateTimePickerFinish.Value;
            }
            fresco.Notes = textBoxObservations.Text;
            dossier.AddConcession(fresco);
            initValues();
        }

        private void checkBoxFinishDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerFinish.Enabled = checkBoxFinishDate.Checked;
        }
    }
}
