using System.Collections.Generic;
using System.Windows.Forms;

namespace GebatWindowComponents.Lists
{
    public class ListaFamiliares : ListaGeneral
    {
        #region//Private Methods

        private void addItem(/*EBFamiliar newitem, int i*/)
        {
            /*ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.DNI);
            item.SubItems.Add(newitem.Name);
            item.SubItems.Add(newitem.Surname);
            item.SubItems.Add(newitem.Age.ToString());
            if (newitem.Gender == MyGender.Male)
            {
                item.SubItems.Add("Hombre");
            }
            else
            {
                item.SubItems.Add("Mujer");
            }
            Items.Add(item);*/
        }

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                addItem((EBFamiliar)it, i);
                i++;
            }*/
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
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                EBFamiliar fam = (EBFamiliar)it;
                if (fam.DNI.Contains(filtro))
                {
                    addItem((EBFamiliar)it, i);
                }
                i++;
            }*/
        }

        #endregion
    }
}