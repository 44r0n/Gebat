using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Combos
{
    public class ComboType : ComboBoxGeneral
    {
        #region//Protected Methods

        /// <summary>
        /// Muestra los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
            Items.Clear();
            foreach (AEN it in colection)
            {
                ENType tipo = (ENType)it;
                this.Items.Add(tipo.Name);
            }
        }

        #endregion

        #region//Public Methods

        public ComboType()
            : base()
        {
        }

        #endregion
    }
}
