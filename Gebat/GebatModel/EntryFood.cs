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
    
    public partial class EntryFood
    {
        public int IdEntryFood { get; set; }
        public int Quantity { get; internal set; }
        public System.DateTime Date { get; internal set; }
        internal int FoodIdFood { private get; set; }
    }
}