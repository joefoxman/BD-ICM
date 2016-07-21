using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public class InstrumentRepository : VersionedRepository<Instrument>, 
        IInstrumentRepository
    {
        public IEnumerable<PartChange> FetchChanges(int instrumentId, DateTime? startDate, DateTime? endDate)
        {
            using (var entities = new BdIcmEntities())
            {
                var results = entities.spFetchInstrumentChanges(instrumentId, startDate, endDate).ToList();
                return results.Select(item => new PartChange
                {
                    ParentPartId = item.ParentPartId,
                    Id = item.PartId.Value,
                    Description = item.Description,
                    DocumentNumber = item.DocumentNumber,
                    ModificationType = item.ModificationType.Value,
                    ModifiedDate = item.ModifiedDate.Value,
                    Modifier = item.Modifier,
                    Name = item.Name,
                    RowVersion = item.RowVersion.Value,
                    EffectiveFrom = item.EffectiveFrom.Value,
                    EffectiveTo = item.EffectiveTo.Value
                }).OrderBy(x => x.ModifiedDate).ToList();
            }
        }

        protected override int GetNewId(Instrument entity)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentVersionRepository>())
            {
                return repository.GetNewId(entity.ModifiedBy);
            }
        }

        public bool HasUncommittedChanges(int instrumentId)
        {
            return false;
        }

        public IEnumerable<ChangeUser> GetUsersWithUncommitedChanges(int instrumentId)
        {
            using (var entities = new BdIcmEntities())
            {
                var results = entities.spFetchUsersWithUncommitedChanges(instrumentId);
                return results.Select(item => new ChangeUser
                {
                    UserId = item.Id, FirstName = item.FirstName, LastName = item.LastName
                }).ToList();
            }
        }

        public IEnumerable<PartSearchResult> SearchInstrumentParts(int instrumentId, string searchKey, int version)
        {
            using (var entities = new BdIcmEntities())
            {
                var results = entities.spSearchInstrumentParts(instrumentId, version, searchKey).ToList();
                return results.Select(Mapper.Map<PartSearchResult>);
            }
        }
    }
}
