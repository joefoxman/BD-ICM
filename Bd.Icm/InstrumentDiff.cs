using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bd.Icm
{
    public class InstrumentDiff
    {
        public Instrument FromInstrument { get; private set; }
        public Instrument ToInstrument { get; private set; }

        public InstrumentCommit FromCommit { get; private set; }
        public InstrumentCommit ToCommit { get; private set; }

        public IList<Part> AddedParts = new List<Part>();
        public IList<Part> DeletedParts = new List<Part>();
        public IList<PartVersion> ModifiedParts = new List<PartVersion>();

        public bool IsDifferent => AddedParts.Any() || DeletedParts.Any() || ModifiedParts.Any();

        public InstrumentDiff(Instrument fromInstrument, Instrument toInstrument,
            InstrumentCommit fromCommit, InstrumentCommit toCommit)
        {
            fromInstrument.ThrowIfNull(nameof(fromInstrument));
            toInstrument.ThrowIfNull(nameof(toInstrument));
            FromInstrument = fromInstrument;
            ToInstrument = toInstrument;
            FromCommit = fromCommit;
            ToCommit = toCommit;
        }

        public async Task CompareAsync()
        {
            if (FromInstrument.InstrumentId != ToInstrument.InstrumentId)
                throw new InvalidOperationException("From and to instruments are not the same ID.");

            await CompareParts(FromInstrument.Parts, ToInstrument.Parts);
        }

        private async Task CompareParts(IList<Part> fromParts, IList<Part> toParts)
        {
            foreach (var fromPart in fromParts)
            {
                var toPart = toParts.SingleOrDefault(x => x.Id.Equals(fromPart.Id));
                if (toPart == null)
                {
                    DeletedParts.Add(fromPart);
                    var deletedPart = Part.GetDeletedPart(fromPart.Id);
                    fromPart.ModifiedDate = deletedPart.ModifiedDate;
                    fromPart.Modifier = deletedPart.Modifier;
                }
            }

            foreach (var toPart in toParts)
            {
                var fromPart = fromParts.SingleOrDefault(x => x.Id.Equals(toPart.Id));
                if (fromPart == null)
                {
                    AddedParts.Add(toPart);
                }
                else
                {
                    if ((fromPart.EffectiveTo != toPart.EffectiveTo) || SettingsChanged(fromPart.Metadata, toPart.Metadata))
                    {
                        ModifiedParts.Add(new PartVersion(fromPart, toPart));
                    }
                    await CompareParts(fromPart.Parts, toPart.Parts);
                }
            }
        }

        private bool SettingsChanged(IList<PartMetadata> fromMetadata, IList<PartMetadata> toMetadata)
        {
            foreach (var setting in fromMetadata)
            {
                var toSetting = toMetadata.SingleOrDefault(x => x.Id.Equals(setting.Id));
                if (toSetting == null) return true;
            }
            foreach (var setting in toMetadata)
            {
                var fromSetting = fromMetadata.SingleOrDefault(x => x.Id.Equals(setting.Id));
                if (fromSetting?.EffectiveTo != setting.EffectiveTo) return true;
            }
            return false;
        }
    }
}
