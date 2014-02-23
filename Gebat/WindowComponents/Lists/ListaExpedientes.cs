using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaExpedientes : ListaGeneral
    {
        #region//Private Methods

        private void addItem(EBPersonalDosier newitem, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.Id[0].ToString());
            item.SubItems.Add(newitem.Income.ToString());
            item.SubItems.Add(newitem.Observations);
            Items.Add(item);
        }

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                addItem((EBPersonalDosier)it, i);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaExpedientes()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("Num. Expediente");
            lista.Add("Ingresos");
            lista.Add("Observaciones");
            Init(lista);
        }

        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                EBPersonalDosier exp = (EBPersonalDosier)it;
                if (exp.Id[0].ToString().Contains(filtro))
                {
                    addItem(exp, i);
                }
                i++;
            }
        }

        #endregion
    }
}
