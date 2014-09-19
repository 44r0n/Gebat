using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using GebatEN.Classes;

namespace GebatWindowComponents.Combos
{
    public class ComboFood : ComboBoxGeneral
    {
        #region//Protected Methods

        /// <summary>
        /// Muestra los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
           /* Items.Clear();
            foreach (AEB it in colection)
            {
                EBFood food = (EBFood)it;
                this.Items.Add(food.Name);
            }*/
        }

        #endregion

        #region//Public Methods

        public ComboFood()
            : base()
        {
        }

        #endregion
    }
}