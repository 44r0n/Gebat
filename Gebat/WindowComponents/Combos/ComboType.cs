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
            foreach (AEB it in colection)
            {
                EBType tipo = (EBType)it;
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
