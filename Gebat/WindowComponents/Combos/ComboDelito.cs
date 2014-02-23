using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Combos
{
    public class ComboDelito : ComboBoxGeneral
    {
        #region//Protected Methods

        /// <summary>
        /// Muestra los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            foreach (AEB it in colection)
            {
                EBCrime delito = (EBCrime)it;
                this.Items.Add(delito.Name);
            }
        }

        #endregion

        #region//Public Methods

        public ComboDelito()
            : base()
        { 
        }

        #endregion
    }
}
