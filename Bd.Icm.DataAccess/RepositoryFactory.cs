using AutoMapper;
using Bd.Icm.DataAccess.Database;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Bd.Icm.DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public static IRepositoryFactory Instance = new RepositoryFactory();
        private static IKernel _kernel;

        public RepositoryFactory()
        {
            _kernel = new StandardKernel();
            BindServices();
        }

        private void BindServices()
        {
            _kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }

        #region IRepositoryFactory Members

        public T GetRepository<T>()
        {
            return _kernel.Get<T>();
        }

        #endregion
    }
}
