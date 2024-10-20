using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferServices _services;

        public TransferController(ITransferServices services, HttpClient httpClient)
        {
            _services = services;

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult>DeleteTransfer(Guid Id)
        {
            try
            {
                var result = await _services.DeleteTransfer(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTransferById(Guid transferId)
        {
            try
            {
                var result = await _services.GetTransferById(transferId);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}/Accounts")]
        public async Task<IActionResult>GetAllTransfersBySrcAccountId(Guid id)
        {
            try
            {
                var result = await _services.GetAllByUser(id);
                return new JsonResult(result) { StatusCode= 200};
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransferResponse), 201)]
        [ProducesResponseType(typeof(List<ExceptionResponse>), 400)]
        public async Task<IActionResult> CreateTransfer(CreateTransferRequest request)
        {
            try
            {
                var result = await _services.CreateTransfer(request);
                return new JsonResult(result)
                {
                    StatusCode = 200
                };
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new ExceptionResponse { message = ex.message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(List<TransferResponse>), 200)]
        public async Task<IActionResult> UpdateTransfer(UpdateTransferRequest request, Guid transferId)
        {
            try
            {
                var result = await _services.UpdateTransfer(request,transferId);
                return new JsonResult(result)
                {
                    StatusCode = 200
                };
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new ExceptionResponse { message = ex.message });
            }
        }
    }
}
