//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSW_Beta_V2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orden()
        {
            this.DetalleOrden = new HashSet<DetalleOrden>();
        }
    
        public int idOrden { get; set; }
        public System.DateTime FechaOrden { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string Cuidad { get; set; }
        public string Distrito { get; set; }
        public string codPostal { get; set; }
        public string Pais { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public Nullable<decimal> total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}