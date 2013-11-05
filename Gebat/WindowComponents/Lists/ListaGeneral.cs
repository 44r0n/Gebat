﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public abstract class ListaGeneral:ListView
    {
        protected List<AEN> colection;
        protected bool init;

        #region//Protected Methods

        /// <summary>
        /// Inicializa las columnas de la lista con la lista que se le pasa por parámetro.
        /// </summary>
        /// <param name="columnNames">Lista de strings con los nombres de las columnas.</param>
        protected void Init(List<string> columnNames)
        {
            if (!init)
            {
                this.Columns.Clear();
                this.Columns.Add("");
                this.Columns[0].Width = 0;
                this.GridLines = true;
                this.View = View.Details;
                this.FullRowSelect = true;
                this.MultiSelect = false;
                int i = 1;
                foreach (string column in columnNames)
                {
                    this.Columns.Add(column);
                    this.Columns[i].TextAlign = HorizontalAlignment.Center;
                    i++;
                }
            }
        }

        /// <summary>
        /// Muestra los todos los elementos.
        /// </summary>
        protected abstract void MostrarElementos();

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene el elemento seleccionado.
        /// </summary>
        public AEN Selected
        {
            get
            {
                return colection[Convert.ToInt32(SelectedItems[0].Text)];
            }
        }

        #endregion

        #region//Public methods

        /// <summary>
        /// Constructor del ListView
        /// </summary>
        public ListaGeneral()
            : base()
        {
            init = false;
            colection = new List<AEN>();
        }

        /// <summary>
        /// Método que actualiza la vista de una lista.
        /// </summary>
        /// <param name="elems">Lista de elementos a mostrar.</param>
        public void Refrescar(List<AEN> elems)
        {
            colection.Clear();
            foreach (AEN item in elems)
            {
                colection.Add(item);
            }
            MostrarElementos();
        }

        /// <summary>
        /// Añade un elemento a la lista.
        /// </summary>
        /// <param name="elem">Elemento a añadir.</param>
        public void Add(AEN elem)
        {
            colection.Add(elem);
            MostrarElementos();
        }

        #endregion
    }
}