//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GebatModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Concession
    {
        public int IdConcession { get; private set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime FinishDate { get; set; }
        public string Observations { get; set; }
        private int PersonalDossierId { get; set; }
    
        public virtual ConcessionType Type { get; set; }
    }
}
