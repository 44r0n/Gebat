﻿using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaFoodIN : ListaGeneral
    {

        #region//Private Methods

        private void addItem(ENFoodIN newitem, int i)
        {
            ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.Nombre);
            item.SubItems.Add(newitem.Fecha.ToShortDateString());
            item.SubItems.Add(newitem.Quantity.ToString());
            Items.Add(item);
        }

        #endregion
        
        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos en la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                addItem((ENFoodIN)it, i);
                i++;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaFoodIN()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("Nombre");
            lista.Add("Fecha");
            lista.Add("Cantidad");
            Init(lista);
        }

        /// <summary>
        /// Filtra la vista con la string especificada.
        /// </summary>
        /// <param name="filtro">Cadena a buscar.</param>
        public void Filter(string filtro)
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENFoodIN entrada = (ENFoodIN)it;
                if (entrada.Nombre.Contains(filtro))
                {
                    addItem((ENFoodIN)it, i);
                }
                i++;
            }
        }

        #endregion
    }
}
