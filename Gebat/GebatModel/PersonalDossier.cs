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
    
    public partial class PersonalDossier
    {
        public PersonalDossier()
        {
            this.Familiar = new HashSet<Familiar>();
            this.Concessions = new HashSet<Concession>();
        }
    
        public int Id { get; set; }
        public string Observations { get; set; }
    
        public virtual ICollection<Familiar> Familiar { get; set; }
        public virtual ICollection<Concession> Concessions { get; set; }
    }
}