using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaDelito:ListaGeneral
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
                ENDelito del = (ENDelito)it;
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(del.Name);
                Items.Add(item);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaDelito()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("Nombre");
            Init(lista);
        }

        /// <summary>
        /// Filtra la vista con la string especificada.
        /// </summary>
        /// <param name="filtro">Filtro</param>
        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENDelito delito = (ENDelito)it;
                if (delito.Name.Contains(filtro))
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Add(delito.Name);
                    Items.Add(item);
                }
                i++;
            }
        }

        #endregion
    }
}
