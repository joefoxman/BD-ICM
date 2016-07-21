using System;
using System.Collections.Generic;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Dto;

namespace Bd.Icm.DataAccess
{
    public interface IInstrumentRepository : IVersionedRepository<Instrument>
    {
        bool HasUncommittedChanges(int instrumentId);
        IEnumerable<ChangeUser> GetUsersWithUncommitedChanges(int instrumentId);
        IEnumerable<PartSearchResult> SearchInstrumentParts(int instrumentId, string searchKey, int version);
        IEnumerable<PartChange> FetchChanges(int instrumentId, DateTime? startDate, DateTime? endDate);
    }
}
