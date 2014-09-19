using System;
using System.Windows.Forms;

namespace GebatWin.Forms
{
    public partial class AddTelfs : Form
    {
        //private AEBPerson persona;

        public AddTelfs(/*AEBPerson persona*/)
        {
            InitializeComponent();
            /*this.Text = "Añadir teléfono a " + persona.Name;
            this.persona = persona;*/
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
        }

        private void buttonAddTelf_Click(object sender, EventArgs e)
        {
            /*this.persona.AddPhone(textBoxTelf.Text);
            textBoxTelf.Text = "";
            textBoxTelf.Focus();
            showTelfs();*/
        }

        private void showTelfs()
        {
            /*listViewTelfs.Items.Clear();
            int i = 0;
            foreach (string telf in persona.Phones)
            {
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(telf);
                listViewTelfs.Items.Add(item);
                i++;
            }*/
        }

        private void buttonDelTelf_Click(object sender, EventArgs e)
        {

           /* persona.DelPhone(listViewTelfs.Items[listViewTelfs.SelectedIndices[0]].SubItems[1].Text);
            showTelfs();*/
        }

        private void AddTelfs_Load(object sender, EventArgs e)
        {
            showTelfs();
        }

        private void listViewTelfs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTelfs.SelectedIndices.Count != 0)
            {
                buttonDelTelf.Enabled = true;
            }

            if (listViewTelfs.SelectedIndices.Count == 0)
            {
                buttonDelTelf.Enabled = false;
            }
        }
    }
}