﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace GebatWindowComponents.Lists
{
    public class ListaFoodIN : ListaGeneral
    {

        #region//Private Methods

        private void addItem(/*EBFoodIN newitem, int i*/)
        {
            /*ListViewItem item = new ListViewItem(i.ToString(), 0);
            item.SubItems.Add(newitem.Name);
            item.SubItems.Add(newitem.Date.ToShortDateString());
            item.SubItems.Add(newitem.Quantity.ToString());
            Items.Add(item);*/
        }

        #endregion
        
        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos en la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                addItem((EBFoodIN)it, i);
                i++;
            }*/
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
            /*Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                EBFoodIN entrada = (EBFoodIN)it;
                if (entrada.Name.Contains(filtro))
                {
                    addItem((EBFoodIN)it, i);
                }
                i++;
            }*/
        }

        #endregion
    }
}