using System;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class InstrumentVersionRepository : Repository<InstrumentVersion>, IInstrumentVersionRepository
    {
        public int GetNewId(int createdBy)
        {
            using (var repository = RepositoryFactory.Instance.GetRepository<IInstrumentVersionRepository>())
            {
                var idEntity = new InstrumentVersion
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = createdBy
                };
                repository.Insert(idEntity);
                return idEntity.InstrumentId;
            }
        }
    }
}
