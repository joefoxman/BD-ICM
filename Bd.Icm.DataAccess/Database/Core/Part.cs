﻿using Bd.Icm.DataAccess.Interfaces;

namespace Bd.Icm.DataAccess.Database
{
    public partial class Part : 
        IVersionedRecord,
        IAuditedRecord,
        ICommittableRecord
    {
    }
}
