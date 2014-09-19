using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace GebatWindowComponents.Lists
{
    public class ListaConcesiones:ListaGeneral
    {

        #region

        private bool isFega;

        #endregion

        #region//Private Methods

        private void addItem(/*AEBConcession newItem, int i*/)
        {
            /*ListViewItem item = new ListViewItem(i.ToString(), 0);
            if (newItem.GetType() == typeof(EBFresco))
            {
                item.SubItems.Add("Fresco");
                isFega = false;
            }

            if (newItem.GetType() == typeof(EBFega))
            {
                item.SubItems.Add("Fega");
                isFega = true;
            }

            item.SubItems.Add(newItem.BeginDate.ToShortDateString());
            if (newItem.FinishDate != new DateTime())
            {
                item.SubItems.Add(newItem.FinishDate.ToShortDateString());
            }
            else
            {
                item.SubItems.Add("");
            }

            if (isFega)
            {
                if (((EBFega)newItem).State == FegaStates.Aproved)
                {
                    item.SubItems.Add("Aprovado");
                }

                if (((EBFega)newItem).State == FegaStates.Awaiting)
                {
                    item.SubItems.Add("Esperando");
                }

                if (((EBFega)newItem).State == FegaStates.Suspended)
                {
                    item.SubItems.Add("Suspendido");
                }
            }
            else
            {
                item.SubItems.Add("");
            }
            item.SubItems.Add(newItem.Notes);
            Items.Add(item);*/
        }

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Muestra todos los elementos de la lista.
        /// </summary>
        protected override void MostrarElementos()
        {
           /* Items.Clear();
            int i = 0;
            foreach (AEB it in colection)
            {
                addItem((AEBConcession)it, i);
                i++;
            }*/
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa los nombres de las columnas.
        /// </summary>
        public ListaConcesiones()
            : base()
        {
            List<string> lista = new List<string>();
            lista.Add("Tipo");
            lista.Add("Fecha Inicio");
            lista.Add("Fecha Fin");
            lista.Add("Estado");
            lista.Add("Observaciones");
            Init(lista);
        }

        #endregion
    }
}