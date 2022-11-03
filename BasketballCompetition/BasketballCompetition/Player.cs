//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasketballCompetition
{
    using System;
    using System.Collections.Generic;
    
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            this.PlayerHistories = new HashSet<PlayerHistory>();
        }
    
        public int Id { get; set; }
        public string Fcs { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Post { get; set; }
        public int GameNumber { get; set; }
        public string Role { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int IdClub { get; set; }
    
        public virtual Club Club { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerHistory> PlayerHistories { get; set; }
    }
}