namespace Bd.Icm.Migrations
{
    public static class DbSchema
    {
        public const string Schema = "dbo";

        public static class Tables
        {
            public const string DbVersion = "DbVersion";
            public const string Part = "Part";
            public const string PartAction = "PartAction";
            public const string PartMetadata = "PartMetadata";
            public const string Instrument = "Instrument";
            public const string InstrumentVersion = "InstrumentVersion";
            public const string PartVersion = "PartVersion";
            public const string PartActionVersion = "PartActionVersion";
            public const string PartMetadataVersion = "PartMetadataVersion";
            public const string User = "User";
            public const string Role = "Role";
            public const string UserRole = "UserRole";
            public const string InstrumentCommit = "InstrumentCommit";
        }

        public static class Columns
        {
            public const string DbVersion = "DbVersion";
            public const string RowVersion = "RowVersion";
            public const string CreatedBy = "CreatedBy";
            public const string CreatedDate = "CreatedDate";
            public const string ModifiedBy = "ModifiedBy";
            public const string ModifiedDate = "ModifiedDate";
            public const string EffectiveFrom = "EffectiveFrom";
            public const string EffectiveTo = "EffectiveTo";
            public const string ModificationType = "ModificationType";
        }
    }
}
