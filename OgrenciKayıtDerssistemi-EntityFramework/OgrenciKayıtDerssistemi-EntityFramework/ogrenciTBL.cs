//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciKayıtDerssistemi_EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ogrenciTBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ogrenciTBL()
        {
            this.notlarTBLs = new HashSet<notlarTBL>();
        }
    
        public int ogrenciID { get; set; }
        public string ogrenciAd { get; set; }
        public string ogrenciSoyad { get; set; }
        public string ogrenciNo { get; set; }
        public string ogrenciBolum { get; set; }
        public string ogrenciTamAD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notlarTBL> notlarTBLs { get; set; }
    }
}
