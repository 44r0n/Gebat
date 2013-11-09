using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaFood : ListaGeneral
    {
        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENFood comida = (ENFood)it;
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(comida.Name);
                item.SubItems.Add(comida.Quantity.ToString() + comida.MyType.Name);
                Items.Add(item);
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
            foreach (AEN it in colection)
            {
                ENFood comida = (ENFood)it;
                if (comida.Name.Contains(filtro))
                {
                    ListViewItem item = new ListViewItem(i.ToString(), 0);
                    item.SubItems.Add(comida.Name);
                    item.SubItems.Add(comida.Quantity.ToString() + comida.MyType.Name);
                    Items.Add(item);
                }
                i++;
            }
        }

        #endregion
    }
}
