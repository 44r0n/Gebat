using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaTypes:ListaGeneral
    {
        #region//Private Methods

        private void addItem(ENType newitem, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.Name);
            Items.Add(item);
        }

        #endregion

        #region//Protected methods

        /// <summary>
        /// Muestra todos los elementos en la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                addItem((ENType)it, i);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaTypes()
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
                ENType tipo = (ENType)it;
                if (tipo.Name.Contains(filtro))
                {
                    addItem((ENType)it, i);
                }
                i++;
            }
        }

        #endregion
    }
}
