using StatusStream.Application.Interfaces;
using StatusStream.Domain.Entities;


namespace StatusStream.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly List<Status> _statuses = new();
        private int _nextId = 1;
        public Status Add(Status status)
        {
            var newStatus = new Status(_nextId++, status.User, status.Message);
            _statuses.Add(newStatus);
            return newStatus;
        }



        public IEnumerable<Status> GetAll(string? user = null)
        {
            return string.IsNullOrWhiteSpace(user)
                ? _statuses
                : _statuses.Where(s => s.User.Equals(user, StringComparison.OrdinalIgnoreCase));
        }
    }
}
