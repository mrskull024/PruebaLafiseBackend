using Microsoft.AspNetCore.Mvc;
using PruebaLafise.API.Models;
using PruebaLafise.Application.Services;
using PruebaLafise.Domain.Entities;
using System.Net;

namespace PruebaLafise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController(ICatalogService service) : ControllerBase
    {
        private readonly ICatalogService _catalogService = service;

        [HttpGet("Genres")]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var result = await _catalogService.GetGenres();

                return Ok(new GenericResponse<List<Genres>>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result ?? []
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno",
                    Data = ex.Message
                });
            }
        }

        [HttpGet("Transactions")]
        public async Task<IActionResult> GetTransaction()
        {
            try
            {
                var result = await _catalogService.GetTransactionTypes();

                return Ok(new GenericResponse<List<TransactionTypes>>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result ?? []
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno",
                    Data = ex.Message
                });
            }
        }


        [HttpGet("Currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var result = await _catalogService.GetCurrencies();

                return Ok(new GenericResponse<List<Currencies>>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result ?? []
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno",
                    Data = ex.Message
                });
            }
        }
    }
}
