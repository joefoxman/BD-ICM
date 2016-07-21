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
    
    public partial class Instrument
    {
        public int Id { get; set; }
        public int RowVersion { get; set; }
        public string Type { get; set; }
        public string NickName { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
        public Nullable<int> SapPartType { get; set; }
        public int ModificationType { get; set; }
        public int MajorRevision { get; set; }
        public int MinorRevision { get; set; }
    
        public virtual InstrumentVersion InstrumentVersion { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Modifier { get; set; }
    }
}