using System.Collections.Generic;
using System.Windows.Forms;
using GebatEN.Classes;

namespace GebatWindowComponents.Lists
{
    public class ListaFoodIN : ListaGeneral
    {
        #region//Protected Methods

        protected override void MostrarElementos()
        {
            Items.Clear();
            int i = 0;
            foreach (AEN it in colection)
            {
                ENFoodIN entrada = (ENFoodIN)it;
            }
        }

        #endregion


    }
}
