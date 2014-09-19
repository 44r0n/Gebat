using System.Collections.Generic;
using System.Windows.Forms;

namespace GebatWindowComponents.Lists
{
    public class ListaTBC : ListaGeneral
    {

        #region//Private Methods

        private void addItem(/*EBTBC newitem, int i*/)
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
            item.SubItems.Add(newitem.Court);
            item.SubItems.Add(newitem.Judgement);
            item.SubItems.Add(newitem.BeginDate.ToShortDateString());
            item.SubItems.Add(newitem.FinishDate.ToShortDateString());
            item.SubItems.Add(newitem.Crime.Name);
            Items.Add(item);*/
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                addItem((EBTBC)it,i);
                i++;
            }*/
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
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                EBTBC tbc = (EBTBC)it;
                if (tbc.DNI.Contains(filtro))
                {
                    addItem((EBTBC)it, i);
                }
                i++;
            }*/
        }

        #endregion
    }
}