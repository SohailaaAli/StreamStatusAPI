using StatusStream.Domain.Entities;


namespace StatusStream.Application.Services
{
    public interface IStatusService
    {
        IEnumerable<Status> GetAll(string? user = null);
        Status Add(Status status);
    }
}
