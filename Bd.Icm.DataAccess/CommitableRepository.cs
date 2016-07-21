using System.Data.Entity;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Interfaces;

namespace Bd.Icm.DataAccess
{
    public abstract class CommittableRepository<T> : VersionedRepository<T>
        where T : class, ICommittableRecord, IVersionedRecord, IAuditedRecord, new()
    {
        protected CommittableRepository()
            : base(new BdIcmEntities())
        {

        }

        protected CommittableRepository(DbContext context)
            : base(context)
        {
        }

        protected override void OnEntityUpdate(T entity)
        {
            entity.InstrumentCommitId = null;
            base.OnEntityUpdate(entity);
        }

        protected override void OnEntityDelete(T entity)
        {
            entity.InstrumentCommitId = null;
            base.OnEntityDelete(entity);
        }
    }
}
