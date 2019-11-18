using System;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

using PostMagnet.Domain.Helpers;
using PostMagnet.Infrastructure.Data.Mapping;

namespace PostMagnet.Infrastructure.Data.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;
        public ISession Session { get; set; }

        static UnitOfWork()
        {
            _sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard.ConnectionString(
                        x => x.FromConnectionStringWithKey("PostMagnetDatabaseConnection")))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<RoleMap>()
                    .AddFromAssemblyOf<PermissionMap>()
                    .AddFromAssemblyOf<EmployeeMap>()
                    .AddFromAssemblyOf<PostMap>()
                    .AddFromAssemblyOf<LogMap>()
                    .AddFromAssemblyOf<InvoiceMap>()
                    .AddFromAssemblyOf<PostExtraPaymentMap>()
                    .AddFromAssemblyOf<WebsiteMap>()
                    .AddFromAssemblyOf<MessageMap>()
                    .AddFromAssemblyOf<NotificationMap>())
                .BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = _sessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Close();
            }
        }
    }
}
