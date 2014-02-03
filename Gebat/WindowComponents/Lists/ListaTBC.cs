using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;
using GebatEN.Enums;

namespace GebatWindowComponents.Lists
{
    public class ListaTBC : ListaGeneral
    {

        #region//Private Methods

        private void addItem(ENTBC newitem, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.DNI);
            item.SubItems.Add(newitem.Nombre);
            item.SubItems.Add(newitem.Apellidos);
            item.SubItems.Add(newitem.Edad.ToString());
            if (newitem.Genero == sexo.Masculino)
            {
                item.SubItems.Add("Hombre");
            }
            else
            {
                item.SubItems.Add("Mujer");
            }
            item.SubItems.Add(newitem.Juzgado);
            item.SubItems.Add(newitem.Ejecutoria);
            item.SubItems.Add(newitem.FInicio.ToShortDateString());
            item.SubItems.Add(newitem.FFin.ToShortDateString());
            item.SubItems.Add(newitem.Delito.Name);
            Items.Add(item);
        }

        #endregion

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
                addItem((ENTBC)it,i);
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
            lista.Add("Edad");
            lista.Add("Sexo");
            lista.Add("Juzgado");
            lista.Add("Ejecutoria");
            lista.Add("Fecha inicio");
            lista.Add("Fecha fin");
            lista.Add("Delito");
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
                    addItem((ENTBC)it, i);
                }
                i++;
            }
        }

        #endregion
    }
}
