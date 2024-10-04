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
        public TransferController(ITransferServices services)
        {
            _services = services;
        }
        [HttpPost]
        [ProducesResponseType(typeof(List<TransferResponse>), 201)]
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
    }
}
