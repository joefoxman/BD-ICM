namespace Bd.Icm.DataAccess
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>();
    }
}
