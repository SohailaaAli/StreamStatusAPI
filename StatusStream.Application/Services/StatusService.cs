using Microsoft.Extensions.Logging;
using StatusStream.Application.Interfaces;
using StatusStream.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusStream.Application.Services
{
    public class StatusService:IStatusService
    {
        private readonly IRepository _repository;
        private readonly ILogger<StatusService> _logger;

        public StatusService(IRepository repository, ILogger<StatusService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Status> GetAll(string? user = null)
        {
            _logger.LogInformation("Fetching statuses{UserFilter}", string.IsNullOrEmpty(user) ? "" : $" for user '{user}'");
            return _repository.GetAll(user);
        }

        public Status Add(Status status)
        {
            _logger.LogInformation("Adding status for user: {User}", status.User);
            return _repository.Add(status);
        }

       

    }
}
