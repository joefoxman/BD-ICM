﻿using System;

namespace Bd.Icm.DataAccess
{
    public class RepositoryBase
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
