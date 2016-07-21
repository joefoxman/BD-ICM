using System;

namespace Bd.Icm.Core
{
    public enum RoleType
    {
        [StringValue("Unknown")] Unknown = 0,
        [StringValue("Read-only")] ReadOnly = 1,
        [StringValue("Contributor")] Contributor = 2,
        [StringValue("Administrator")] Administrator = 3,
        [StringValue("Committer")] Committer = 4
    }

    public enum ModificationType
    {
        [StringValue("Unknown")] Unknown = 0,
        [StringValue("Added")] Insert = 1,
        [StringValue("Modified")] Update = 2,
        [StringValue("Deleted")] Delete = 3
    }

    public enum RecordType
    {
        [StringValue("Instrument")] Instrument = 1,
        [StringValue("Part")] Part = 2,
        [StringValue("Setting")] PartMetadata = 3,
        [StringValue("Action")] PartAction = 4
    }

    public enum InstrumentType
    {
        [StringValue("N/A")] None = 0,
        [StringValue("FERT")] Fert = 1
    }

    public enum PartType
    {
        [StringValue("N/A")] None = 0,
        [StringValue("HALB")] Halb = 1,
        [StringValue("ROH")] Roh = 2,
        [StringValue("ZICP")] Zicp = 3
    }

    public enum PartActionType
    {
        [StringValue("None")] None = 0,
        [StringValue("Calibration")] Calibration = 1,
        [StringValue("Repair")] Repair = 2,
        [StringValue("Settings")] Settings = 3
    }

    public static class StringEnum
    {
        public static string StringValue(this Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());
            var attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if ((attrs != null) && (attrs.Length > 0))
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

}
