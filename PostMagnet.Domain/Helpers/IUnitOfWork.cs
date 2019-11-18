namespace PostMagnet.Domain.Helpers
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
    }
}
