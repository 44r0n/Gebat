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
    public partial class ChangeState : Form
    {
        EBFega fega;

        public ChangeState(EBFega fega)
        {
            InitializeComponent();
            this.fega = fega;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            fega.Save();
            this.Close();
        }
    }
}
