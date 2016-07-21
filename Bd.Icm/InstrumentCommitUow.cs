using System;
using System.Linq;
using System.Transactions;
using Bd.Icm.DataAccess;
using Csla;
using JetBrains.Annotations;

namespace Bd.Icm
{
    [Serializable]
    public class InstrumentCommitUow : BusinessBase<InstrumentCommitUow>
    {

        public static readonly PropertyInfo<Instrument> InstrumentProperty = RegisterProperty<Instrument>(c => c.Instrument);
        public static readonly PropertyInfo<PartChanges> ChangesProperty = RegisterProperty<PartChanges>(c => c.Changes);
        public static readonly PropertyInfo<PartMetadataChanges> MetadataChangesProperty = RegisterProperty<PartMetadataChanges>(c => c.MetadataChanges);
        public static readonly PropertyInfo<InstrumentCommit> CommitProperty = RegisterProperty<InstrumentCommit>(c => c.Commit);
        public static readonly PropertyInfo<int> LastChangeVersionProperty = RegisterProperty<int>(c => c.LastChangeVersion);

        public int LastChangeVersion
        {
            get { return GetProperty(LastChangeVersionProperty); }
            set { SetProperty(LastChangeVersionProperty, value); }
        }

        public InstrumentCommit Commit
        {
            get { return GetProperty(CommitProperty); }
            set { SetProperty(CommitProperty, value); }
        }

        public PartChanges Changes
        {
            get { return LazyGetProperty(ChangesProperty, PartChanges.NewPartChanges); }
        }

        public PartMetadataChanges MetadataChanges
        {
            get { return LazyGetProperty(MetadataChangesProperty, PartMetadataChanges.NewPartMetadataChanges); }
        }

        public Instrument Instrument
        {
            get { return GetProperty(InstrumentProperty); }
            set { SetProperty(InstrumentProperty, value); }
        }

        public static InstrumentCommitUow Create(int instrumentId)
        {
            return DataPortal.Fetch<InstrumentCommitUow>(instrumentId);
        }

        public InstrumentCommitUow()
        {
            
        }

        [UsedImplicitly]
        private void DataPortal_Fetch(int instrumentId)
        {
            LoadProperty(CommitProperty, InstrumentCommit.NewInstrumentCommit());
            Commit.InstrumentId = instrumentId;
            LoadProperty(InstrumentProperty, Instrument.GetInstrument(instrumentId));
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartRepository>())
            {
                var changes = repository.GetUncommittedChanges(instrumentId);
                LoadProperty(ChangesProperty, PartChanges.GetPartChanges(changes));
            }
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataRepository>())
            {
                var changes = repository.GetUncommittedChanges(instrumentId);
                LoadProperty(MetadataChangesProperty, PartMetadataChanges.GetPartMetadataChanges(changes));
            }
            MarkDirty();
        }

        protected override void DataPortal_Update()
        {
            using (var ts = new TransactionScope())
            {
                Commit = Commit.Save();
                foreach (var partChange in Changes.Where(x => x.EffectiveFrom <= LastChangeVersion))
                {
                    partChange.Commit(Commit.Id);
                }
                foreach (var metadataChange in MetadataChanges.Where(x => x.EffectiveFrom <= LastChangeVersion))
                {
                    metadataChange.Commit(Commit.Id);
                }
                FieldManager.UpdateChildren();
                Instrument.MajorRevision++;
                Instrument = Instrument.Save();
                Commit.Revision = Instrument.MajorRevision;
                Commit.EffectiveTo = LastChangeVersion;
                Commit = Commit.Save();
                ts.Complete();
            }
        }

    }
}
