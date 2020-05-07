namespace Backend.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}