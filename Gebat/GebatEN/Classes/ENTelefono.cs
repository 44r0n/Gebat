using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class ENTelefono : AEN
    {
        #region//Atributes

        private string numero;
        private AENPersona dueño;

        #endregion

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRowde forma que corresponde a la base de datos.
        /// </summary>
        protected override DataRow ToRow
        {
            get 
            {
                DataRow ret = cad.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["Numero"] = this.numero;
                ret["DNI"] = this.dueño.DNI;
                return ret;
            }
        }


    }
}
