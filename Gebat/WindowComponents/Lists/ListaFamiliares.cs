using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;
using GebatEN.Enums;

namespace GebatWindowComponents.Lists
{
    public class ListaFamiliares : ListaGeneral
    {
        #region//Private Methods

        private void addItem(ENFamiliar newitem, int i)
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
            foreach (AEN it in colection)
            {
                addItem((ENFamiliar)it, i);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaFamiliares()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("DNI");
            lista.Add("Nombre");
            lista.Add("Apellidos");
            lista.Add("Edad");
            lista.Add("Sexo");
            Init(lista);
        }

        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENFamiliar fam = (ENFamiliar)it;
                if (fam.DNI.Contains(filtro))
                {
                    addItem((ENFamiliar)it, i);
                }
                i++;
            }
        }

        #endregion
    }
}
