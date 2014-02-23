using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Combos
{
    public abstract class ComboBoxGeneral : ComboBox
    {
        protected List<AEB> colection;

        #region//Protected Methods

        protected abstract void MostrarElementos();

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene el elemento seleccionado.
        /// </summary>
        public AEB Selected
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                return colection[this.SelectedIndex];
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor del Combobox.
        /// </summary>
        public ComboBoxGeneral()
            : base()
        {
            colection = new List<AEB>();
        }

        /// <summary>
        /// Mñetodo que actualiza la vista del combobox.
        /// </summary>
        /// <param name="elems"></param>
        public void Refrescar(List<AEB> elems)
        {
            colection.Clear();
            foreach (AEB item in elems)
            {
                colection.Add(item);
            }
            MostrarElementos();
        }

        /// <summary>
        /// Añade un elemento a la lista.
        /// </summary>
        /// <param name="elem">Elemento a añadir.</param>
        public void Add(AEB elem)
        {
            colection.Add(elem);
            MostrarElementos();
        }

        #endregion
    }
}
