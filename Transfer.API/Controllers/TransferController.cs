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
        private readonly HttpClient _httpClient;
        public TransferController(ITransferServices services, HttpClient httpClient)
        {
            _services = services;
            _httpClient = httpClient;
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
