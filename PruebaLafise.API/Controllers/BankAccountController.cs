using Microsoft.AspNetCore.Mvc;
using PruebaLafise.API.Models;
using PruebaLafise.Application.Services;
using PruebaLafise.Domain.Entities;
using System.Net;

namespace PruebaLafise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController(IBankAccountService accountService, ICatalogService catalogService) : ControllerBase
    {
        private readonly IBankAccountService _accountService = accountService;
        private readonly ICatalogService _catalogService = catalogService;

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(BankAccount account)
        {
            try
            {
                var checkAccountExists = await _accountService.CheckAccountNumber(account.AccountNumber);

                if (checkAccountExists is not null)
                {
                    return Ok(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "El numero de cuenta proporcionado no esta disponible"
                    });
                }

                var result = await _accountService.Create(account);

                return Ok(new GenericResponse<BankAccount>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result
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

        [HttpGet("Balance/{accountNumber}")]
        public async Task<IActionResult> GetBalance(string accountNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(accountNumber))
                {
                    return Ok(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Debe Ingresar un numero de cuenta"
                    });
                }

                var result = await _accountService.GetBalance(accountNumber.Trim());

                return Ok(new GenericResponse
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result
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

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(AccountMovements movement)
        {
            try
            {
                if (string.IsNullOrEmpty(movement.AccountNumber))
                {
                    return BadRequest(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Debe Ingresar un numero de cuenta"
                    });
                }

                var currentBalance = await _accountService.GetBalance(movement.AccountNumber.Trim());
                var newBalance = currentBalance + movement.MovementAmmount;
                movement.CurrentBalance = newBalance;
                movement.IsAuthorized = true;
                movement.TransactionId = new Guid();

                var result = await _accountService.Deposit(movement);
                var updateAmmount = _accountService.UpdateBalance(movement.AccountNumber.Trim(), newBalance);

                return Ok(new GenericResponse<BankAccount>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa"
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

        [HttpPost("Withdrawal")]
        public async Task<IActionResult> Withdrawal(AccountMovements movement)
        {
            try
            {
                if (string.IsNullOrEmpty(movement.AccountNumber))
                {
                    return BadRequest(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Debe Ingresar un numero de cuenta"
                    });
                }

                var currentBalance = await _accountService.GetBalance(movement.AccountNumber.Trim());

                if (currentBalance < movement.MovementAmmount)
                {
                    return BadRequest(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = $"El monto del retiro excede el saldo disponible, su saldo actual es de: {currentBalance} "
                    });
                }
                var newBalance = currentBalance - movement.MovementAmmount;
                movement.CurrentBalance = newBalance;
                movement.IsAuthorized = true;
                movement.TransactionId = new Guid();

                var result = await _accountService.Withdrawal(movement);
                var updateAmmount = _accountService.UpdateBalance(movement.AccountNumber.Trim(), newBalance);

                return Ok(new GenericResponse<BankAccount>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa"
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

        [HttpGet("ResumeInfo")]
        public async Task<IActionResult> Resume(int userId, string accountNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(accountNumber))
                {
                    return Ok(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Debe Ingresar un numero de cuenta"
                    });
                }

                var result = await _accountService.Resume(userId, accountNumber);

                foreach (var item in result.Movements)
                {
                    item.TransactionDescription = GetTransactionTypeDescription(item.TransactionType).Result;
                }

                return Ok(new GenericResponse<TransactionResume>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Operación exitosa",
                    Data = result
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

        private async Task<string> GetTransactionTypeDescription(int transactionTypeId)
        {
            var result = await _catalogService.GetTransactionTypes();
            return result.Where(t => t.Id == transactionTypeId)
                  .Select(t => t.Name).FirstOrDefault();
        }
    }
}
