using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaFood : ListaGeneral
    {

        #region//Private Methods

        private void addItem(EBFood newitem,int i)
        {
            ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.Name);
            item.SubItems.Add(newitem.Quantity.ToString() + " " + newitem.MyType.Name);
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
                addItem((EBFood)it, i);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa lso nombres de las columnas.
        /// </summary>
        public ListaFood()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("Nombre");
            lista.Add("Cantidad");
            Init(lista);
        }

        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                EBFood comida = (EBFood)it;
                if (comida.Name.Contains(filtro))
                {
                    addItem((EBFood)it, i);
                }
                i++;
            }
        }

        #endregion
    }
}
