using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public interface IInstrumentVersionRepository : IRepository<InstrumentVersion>
    {
        int GetNewId(int createdBy);
    }
}
