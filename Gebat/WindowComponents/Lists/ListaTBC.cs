using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaTBC : ListaGeneral
    {
        #region Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENTBC tbc = (ENTBC)it;
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(tbc.DNI);
                item.SubItems.Add(tbc.Nombre);
                item.SubItems.Add(tbc.Apellidos);
                item.SubItems.Add(tbc.Juzgado);
                item.SubItems.Add(tbc.Ejecutoria);
                item.SubItems.Add(tbc.FInicio.ToShortDateString());
                item.SubItems.Add(tbc.FFin.ToShortDateString());
                Items.Add(item);
                i++;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaTBC()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("DNI");
            lista.Add("Nombre");
            lista.Add("Apellidos");
            lista.Add("Juzgado");
            lista.Add("Ejecutoria");
            lista.Add("Fecha inicio");
            lista.Add("Fecha fin");
            Init(lista);
        }

        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENTBC tbc = (ENTBC)it;
                if (tbc.DNI.Contains(filtro))
                {
                    ListViewItem item = new ListViewItem(i.ToString(), 0);
                    item.SubItems.Add(tbc.DNI);
                    item.SubItems.Add(tbc.Nombre);
                    item.SubItems.Add(tbc.Apellidos);
                    item.SubItems.Add(tbc.Juzgado);
                    item.SubItems.Add(tbc.Ejecutoria);
                    item.SubItems.Add(tbc.FInicio.ToShortDateString());
                    item.SubItems.Add(tbc.FFin.ToShortDateString());
                    Items.Add(item);
                }
                i++;
            }
        }

        #endregion
    }
}
