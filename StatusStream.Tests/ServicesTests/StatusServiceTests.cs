using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using StatusStream.Application.Interfaces;
using StatusStream.Application.Services;
using StatusStream.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StatusStream.Tests.ServicesTests
{
    public class StatusServiceTests
    {
        private readonly Mock<IRepository> _mockRepo;
        private readonly Mock<ILogger<StatusService>> _mockLogger;
        private readonly StatusService _service;

        public StatusServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockLogger = new Mock<ILogger<StatusService>>();
            _service = new StatusService(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public void Add_ShouldCallRepositoryAndReturnStatus()
        {
            var status = new Status(0, "Sohaila", "Hello!");
            _mockRepo.Setup(r => r.Add(It.IsAny<Status>()))
                     .Returns((Status s) => s);

            var result = _service.Add(status);

            result.Should().NotBeNull();
            result.User.Should().Be("Sohaila");
            _mockRepo.Verify(r => r.Add(It.IsAny<Status>()), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnAllStatuses()
        {
            var statuses = new List<Status>
            {
                new Status(1, "User1", "Message1"),
                new Status(2, "User2", "Message2")
            };
            _mockRepo.Setup(r => r.GetAll(null)).Returns(statuses);

            var result = _service.GetAll();

            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetAll_ByUser_ShouldReturnFiltered()
        {
            var filtered = new List<Status>
            {
                new Status(1, "Sohaila", "Hi")
            };
            _mockRepo.Setup(r => r.GetAll("Sohaila")).Returns(filtered);

            var result = _service.GetAll("Sohaila");

            result.Should().OnlyContain(s => s.User == "Sohaila");
        }
    }
}

