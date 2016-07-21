//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bd.Icm.DataAccess.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartAction
    {
        public int Id { get; set; }
        public int RowVersion { get; set; }
        public int PartId { get; set; }
        public int Action { get; set; }
        public string Description { get; set; }
        public System.DateTime ActionDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
        public int ModificationType { get; set; }
        public Nullable<int> InstrumentCommitId { get; set; }
    
        public virtual PartActionVersion PartActionVersion { get; set; }
        public virtual PartVersion PartVersion { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Modifier { get; set; }
        public virtual InstrumentCommit InstrumentCommit { get; set; }
    }
}
