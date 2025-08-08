using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StatusStream.API.Controllers;
using StatusStream.Application.DTOs;
using StatusStream.Application.Services;
using StatusStream.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StatusStream.Tests.ControllerTests
{
    public class StatusControllerTests
    {
        private readonly Mock<IStatusService> _mockService;
        private readonly StatusController _controller;

        public StatusControllerTests()
        {
            _mockService = new Mock<IStatusService>();
            _controller = new StatusController(_mockService.Object);
        }

        [Fact]
        public void PostStatus_ValidDto_ReturnsCreated()
        {
            var dto = new StatusDto { User = "Ali", Message = "Test" };
            var saved = new Status(1, "Ali", "Test");

            _mockService.Setup(s => s.Add(It.IsAny<Status>())).Returns(saved);

            var result = _controller.PostStatus(dto) as CreatedAtActionResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(201);
            var response = result.Value as StatusResponseDto;
            response!.User.Should().Be("Ali");
        }

        [Fact]
        public void PostStatus_InvalidDto_ReturnsBadRequest()
        {
            var dto = new StatusDto { User = "", Message = "" };

            var result = _controller.PostStatus(dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void GetAllStatuses_ReturnsList()
        {
            var data = new List<Status>
            {
                new Status(1, "Ali", "Hello"),
                new Status(2, "Sohaila", "Hi")
            };
            _mockService.Setup(s => s.GetAll(null)).Returns(data);

            var result = _controller.GetAllStatuses() as OkObjectResult;

            result.Should().NotBeNull();
            var items = result!.Value as IEnumerable<StatusResponseDto>;
            items!.Should().HaveCount(2);
        }

        [Fact]
        public void GetAllStatuses_FilteredByUser_ReturnsOnlyMatching()
        {
            var data = new List<Status>
            {
                new Status(1, "Sohaila", "Hello")
            };
            _mockService.Setup(s => s.GetAll("Sohaila")).Returns(data);

            var result = _controller.GetAllStatuses("Sohaila") as OkObjectResult;

            result.Should().NotBeNull();
            var list = result!.Value as IEnumerable<StatusResponseDto>;
            list!.All(x => x.User == "Sohaila").Should().BeTrue();
        }
    }
}