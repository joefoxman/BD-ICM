using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public class PartRepository : CommittableRepository<Part>, IPartRepository
    {
        public IEnumerable<Part> FetchAllByParent(int parentId, int version)
        {
            return FetchAll(version).Where(x => x.ParentPartId == parentId);
        }

        public IEnumerable<Part> FetchAllByInstrument(int instrumentId, int version)
        {
            return FetchAll(version).Where(x => x.InstrumentId == instrumentId);
        }

        public IEnumerable<PartNode> FetchPartHeirarchy(int? instrumentId, int? parentPartId, int version)
        {
            var nodes = new List<PartNode>();

            if (instrumentId.HasValue)
            {
                nodes.Add(GetInstrument(instrumentId.Value, version));
            }
            if (parentPartId.HasValue)
            {
                using (var entities = new BdIcmEntities())
                {
                    var partNodes = entities.spFetchPartHeirarchy(parentPartId.Value, version).ToList();
                    foreach (var partNode in partNodes)
                    {
                        nodes.Add(new PartNode
                        {
                            PartId = partNode.PartId.Value,
                            ParentPartId = partNode.ParentPartId,
                            InstrumentId = partNode.InstrumentId,
                            Name = partNode.Name,
                            Description = partNode.Description,
                            Level = partNode.Level.Value
                        });
                    }

                    if (instrumentId.HasValue == false)
                    {
                        var instrumentNode = partNodes.Single(x => x.InstrumentId.HasValue);
                        nodes.Add(GetInstrument(instrumentNode.InstrumentId.Value, version));
                    }
                }
            }
            return nodes.OrderByDescending(x => x.Level);
        }

        private PartNode GetInstrument(int instrumentId, int version)
        {
            using (var instrumentRepo = RepositoryFactory.Instance.GetRepository<IInstrumentRepository>())
            {
                var instrument = instrumentRepo.Single(x => x.Id == instrumentId, version);
                return new PartNode
                {
                    InstrumentId = instrument.Id,
                    Name = instrument.Type,
                    Level = int.MaxValue
                };
            }

        }

        protected override int GetNewId(Part entity)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IPartVersionRepository>())
            {
                return repository.GetNewId(entity.ModifiedBy);
            }
        }

        public IEnumerable<PartChange> GetUncommittedChanges(int instrumentId)
        {
            using (var entities = new BdIcmEntities())
            {
                var results = entities.spFetchUncommittedPartChanges(instrumentId);
                return results.Select(item => new PartChange
                {
                    Id = item.PartId.Value,
                    ParentPartId = item.ParentPartId,
                    Name = item.Name,
                    Description = item.Description,
                    DocumentNumber = item.DocumentNumber,
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

        private PartChange ExportPartChange(Part part)
        {
            return new PartChange
            {
                ParentPartId = part.ParentPartId,
                Name = part.Name,
                Description = part.Description,
                DocumentNumber = part.DocumentNumber,
                ModifiedDate = part.ModifiedDate,
                ModificationType = part.ModificationType,
            };
        }

        public PartChange Commit(PartChange entity)
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
                return ExportPartChange(currentEntity);
            }
        }

    }
}
