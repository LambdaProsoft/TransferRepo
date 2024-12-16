using Application.Interfaces;
using Application.Response;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.API.Controllers;

namespace UnitTest
{
    public class StatusAndTypeTests
    {
        private ITransferStatusServices _statusServices;
        private TransferStatusController _statusController;
        private ITransferTypeServices _typeServices;
        private TransferTypeController _typeController;
        private HttpClient _httpClient;

        public StatusAndTypeTests()
        {
            //dependencies
            _statusServices = A.Fake<ITransferStatusServices>();
            _typeServices = A.Fake<ITransferTypeServices>();
            _httpClient = A.Fake<HttpClient>();

            //SUT
            _statusController = new TransferStatusController(_statusServices,_httpClient);
            _typeController = new TransferTypeController(_typeServices,_httpClient);
            
        }
        [Fact]
        public async Task GetTransferStatuses_ShouldReturn200_WhenStatusesAreReturnedSuccessfully()
        {
            // Arrange
            var fakeStatuses = new List<TransferStatusResponse>
            {
                new TransferStatusResponse { Id = 1, Status = "Pending" },
                new TransferStatusResponse { Id = 2, Status = "Completed" }
            };

            A.CallTo(() => _statusServices.GetAll())
                .Returns(Task.FromResult(fakeStatuses));

            // Act
            var result = await _statusController.GetTransferStatuses() as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<TransferStatusResponse>>()
                .Which.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTransferStatuses_ShouldReturn200_WhenTypesAreReturnedSuccessfully()
        {
            // Arrange
            var fakeTypes = new List<TransferTypeResponse>
            {
                new TransferTypeResponse { TransferTypeId = 1, Name = "Name1" },
                new TransferTypeResponse { TransferTypeId = 2, Name = "Name2" }
            };

            A.CallTo(() => _typeServices.GetAll())
                .Returns(Task.FromResult(fakeTypes));

            // Act
            var result = await _typeController.GetTransferStatuses() as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<TransferTypeResponse>>()
                .Which.Should().HaveCount(2);
        }
    }


}

