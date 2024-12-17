using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Transfer.API.Controllers;

namespace UnitTest
{
    public class TransferControllerTest1
    {
        private TransferController _transferController;
        private ITransferServices _services;
        private HttpClient _httpClient;

        public TransferControllerTest1()
        {
            //dependencies
            _services = A.Fake<ITransferServices>();
            _httpClient = A.Fake<HttpClient>();
            //SUT
            _transferController = new TransferController(_services,_httpClient);
        }

        [Fact]
        public async void TransferController_GetTransferById_ReturnsSuccess()
        {
            //Arrange
            var transferId = Guid.NewGuid();
            var fakeResponse = new TransferResponse { Id = transferId, Amount = 100 };

            // Configura el servicio para devolver un resultado
            A.CallTo(() => _services.GetTransferById(transferId))
                .Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await _transferController.GetTransferById(transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<TransferResponse>();
            
            result.Value.Should().BeOfType<TransferResponse>()
                .Which.Id.Should().Be(transferId);
        }

        [Fact]
        public async Task TransferController_GetTransferById_ShouldReturn404()
        {
            // Arrange
            var transferId = Guid.NewGuid();


            // Configura el servicio para lanzar una excepción
            A.CallTo(() => _services.GetTransferById(transferId))
                .Throws(new ExceptionNotFound("There's no transfer with that Id"));

            // Act
            var result = await _transferController.GetTransferById(transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull(); 
            result!.StatusCode.Should().Be(404); 
            result.Value.Should().BeOfType<ApiError>();
            var errorResponse = result.Value as ApiError;
            errorResponse.Message.Should().Be("There's no transfer with that Id"); 
        }

        [Fact]
        public async Task TransferController_DeleteTransfer_ShouldReturn200()
        {
            // Arrange
            var transferId = Guid.NewGuid();
            var fakeResponse = new TransferResponse { Id = transferId, Amount = 100 };

            // Configura el servicio para devolver un resultado exitoso
            A.CallTo(() => _services.DeleteTransfer(transferId))
                .Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await _transferController.DeleteTransfer(transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200); // Código de estado 200
            result.Value.Should().BeOfType<TransferResponse>()
                .Which.Id.Should().Be(transferId); // Validación del objeto TransferResponse
        }

        [Fact]
        public async Task TransferController_DeleteTransfer_ShouldReturn404()
        {
            // Arrange
            var transferId = Guid.NewGuid();
            var exceptionMessage = "Transfer not found";

            // Configura el servicio para lanzar una excepción de tipo ExceptionNotFound
            A.CallTo(() => _services.DeleteTransfer(transferId))
                .Throws(new ExceptionNotFound(exceptionMessage));

            // Act
            var result = await _transferController.DeleteTransfer(transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(404); // Código de estado 404
            result.Value.Should().BeOfType<ApiError>()
                .Which.Message.Should().Be(exceptionMessage); // Validación del mensaje de error
        }
        [Fact]
        public async Task TransferController_GetAllTransfersBySrcAccountId_ShouldReturn200()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var offset = 0;
            var size = 0;
            var fakeTransfers = new List<TransferResponse>
            {
                new TransferResponse { Id = Guid.NewGuid(), Amount = 100 },
                new TransferResponse { Id = Guid.NewGuid(), Amount = 200 }
            };

            A.CallTo(() => _services.GetAllByAccount(accountId, offset, size))
                .Returns(Task.FromResult(fakeTransfers));

            // Act
            var result = await _transferController.GetAllTransfersBySrcAccountId(accountId, offset, size) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<TransferResponse>>()
                .Which.Should().HaveCount(2);
        }

        [Fact]
        public async Task TransferController_GetAllTransfersBySrcAccountId_ShouldReturn404()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var offset = 0;
            var size = 10;
            var exceptionMessage = "Conflict occurred";

            A.CallTo(() => _services.GetAllByAccount(accountId, offset, size))
                .Throws(new Conflict(exceptionMessage));

            // Act
            var result = await _transferController.GetAllTransfersBySrcAccountId(accountId, offset, size) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(404);
            result.Value.Should().BeOfType<ApiError>()
                .Which.Message.Should().Be(exceptionMessage);
        }
        [Fact]
        public async Task TransferController_CreateTransfer_ShouldReturn201()
        {
            // Arrange
            var request = new CreateTransferRequest { SrcAccountId = Guid.NewGuid(), DestAccountAliasOrCBU = Guid.NewGuid().ToString(), Amount = 100,Description="description",TypeId=1};
            var fakeResponse = new TransferResponse { Id = Guid.NewGuid(), Amount = 100 };

            A.CallTo(() => _services.CreateTransfer(request))
                .Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await _transferController.CreateTransfer(request) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(201);
            result.Value.Should().BeOfType<TransferResponse>()
                .Which.Id.Should().Be(fakeResponse.Id);
        }

        [Fact]
        public async Task TransferController_CreateTransfer_ShouldReturn404_WhenConflictOccurs()
        {
            // Arrange
            var request = new CreateTransferRequest { SrcAccountId = Guid.NewGuid(), DestAccountAliasOrCBU = Guid.NewGuid().ToString(), Amount = 100, Description = "description", TypeId = 1 };
            var exceptionMessage = "Conflict occurred";

            A.CallTo(() => _services.CreateTransfer(request))
                .Throws(new Conflict(exceptionMessage));

            // Act
            var result = await _transferController.CreateTransfer(request) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(404);
            result.Value.Should().BeOfType<ApiError>()
                .Which.Message.Should().Be(exceptionMessage);
        }

        [Fact]
        public async Task CreateTransfer_ShouldReturn404_WhenAccountErrorOccurs()
        {
            // Arrange
            var request = new CreateTransferRequest { SrcAccountId = Guid.NewGuid(), DestAccountAliasOrCBU = Guid.NewGuid().ToString(), Amount = 100, Description = "description", TypeId = 1 };
            var exceptionMessage = "Account error occurred";

            A.CallTo(() => _services.CreateTransfer(request))
                .Throws(new AccountErrorException(exceptionMessage));

            // Act
            var result = await _transferController.CreateTransfer(request) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(404);
            result.Value.Should().BeOfType<ApiError>()
                .Which.Message.Should().Be(exceptionMessage);
        }
        [Fact]
        public async Task UpdateTransfer_ShouldReturn200_WhenTransferIsUpdatedSuccessfully()
        {
            // Arrange
            var transferId = Guid.NewGuid();
            var request = new UpdateTransferRequest { Description ="descrption2",TypeId=2 };
            var fakeResponse = new TransferResponse { Id = transferId, Amount = 150};

            A.CallTo(() => _services.UpdateTransfer(request, transferId))
                .Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await _transferController.UpdateTransfer(request, transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<TransferResponse>()
                .Which.Id.Should().Be(transferId);
        }

        [Fact]
        public async Task UpdateTransfer_ShouldReturn404_WhenConflictOccurs()
        {
            // Arrange
            var transferId = Guid.NewGuid();
            var request = new UpdateTransferRequest { Description = "description2", TypeId = 2 };
            var exceptionMessage = "Conflict occurred";

            A.CallTo(() => _services.UpdateTransfer(request, transferId))
                .Throws(new Conflict(exceptionMessage));

            // Act
            var result = await _transferController.UpdateTransfer(request, transferId) as JsonResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(404);
            result.Value.Should().BeOfType<ApiError>()
                .Which.Message.Should().Be(exceptionMessage);
        }
    }
}
