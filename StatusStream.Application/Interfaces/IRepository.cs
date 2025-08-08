using StatusStream.Domain.Entities;


namespace StatusStream.Application.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Status> GetAll(string? user = null);
        Status Add(Status status);
    }
}
