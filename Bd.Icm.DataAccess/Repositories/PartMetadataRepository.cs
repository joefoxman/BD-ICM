using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public class PartMetadataRepository : CommittableRepository<PartMetadata>, IPartMetadataRepository
    {
        protected override int GetNewId(PartMetadata entity)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartMetadataVersionRepository>())
            {
                return repository.GetNewId(entity.ModifiedBy);
            }
        }

        public IEnumerable<PartMetadata> FetchAll(int partId, int version)
        {
            return FetchAll(version).Where(x => x.PartId == partId);
        }

        public IEnumerable<PartMetadataChange> GetUncommittedChanges(int instrumentId)
        {
            using (var entities = new BdIcmEntities())
            {
                var results = entities.spFetchUncommittedMetadataChanges(instrumentId);
                return results.Select(item => new PartMetadataChange
                {
                    Id = item.PartMetadataId.Value,
                    PartId = item.PartId.Value,
                    Name = item.Name,
                    Description = item.Description,
                    MetaKey = item.MetaKey,
                    MetaValue = item.MetaValue,
                    ModifiedDate = item.ModifiedDate.Value,
                    Modifier = item.Modifier,
                    CreatedDate = item.CreatedDate.Value,
                    Creator = item.Creator,
                    ModificationType = item.ModificationType.Value,
                    RowVersion = item.RowVersion.Value,
                    EffectiveFrom = item.EffectiveFrom.Value,
                    EffectiveTo = item.EffectiveTo.Value
                }).OrderBy(o => o.ModifiedDate).ToList();
            }
        }

        private PartMetadataChange ExportPartMetadataChange(PartMetadata metadata)
        {
            return new PartMetadataChange
            {
                Id = metadata.Id,
                PartId = metadata.PartId,
                MetaKey = metadata.MetaKey,
                MetaValue = metadata.MetaValue,
                ModifiedDate = metadata.ModifiedDate,
                CreatedDate = metadata.CreatedDate,
                ModificationType = metadata.ModificationType,
                RowVersion = metadata.RowVersion
            };
        }

        public PartMetadataChange Commit(PartMetadataChange entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (var transaction = new TransactionScope())
            {
                var currentEntity = Fetch().Single(x => (x.Id == entity.Id) && (x.RowVersion == entity.RowVersion));
                currentEntity.InstrumentCommitId = entity.InstrumentCommitId;
                _dbSet.Attach(currentEntity);
                _context.Entry(currentEntity).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Complete();
                return ExportPartMetadataChange(currentEntity);
            }
        }

    }
}
