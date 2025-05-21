using Microsoft.AspNetCore.Mvc;
using PruebaLafise.API.Models;
using PruebaLafise.Application.Services;
using PruebaLafise.Domain.Entities;
using System.Net;

namespace PruebaLafise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfileController(IUserProfileService userService) : ControllerBase
    {
        private readonly IUserProfileService _userService = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Usuario no encontrado"
                    });
                }

                var result = await _userService.GetById(id);

                if (result == null)
                {
                    return NotFound(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Message = "Usuario no encontrado"
                    });
                }

                return Ok(new GenericResponse<UserProfile>
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userService.GetAll();

                return Ok(new GenericResponse<List<UserProfile>>
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

        [HttpPost]
        public async Task<IActionResult> Save(UserProfile user)
        {
            try
            {
                var result = await _userService.Save(user);

                return Ok(new GenericResponse<UserProfile>
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserProfile user, int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new GenericResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Usuario no encontrado"
                    });
                }
                else
                {
                    var findUser = _userService.GetById(id);
                    if (findUser is null)
                    {
                        return NotFound(new GenericResponse<UserProfile>
                        {
                            Status = (int)HttpStatusCode.NotFound,
                            Message = $"El id de usuario: {id} a actulizar no existe"
                        });
                    }
                }

                user.Id = id;

                var result = await _userService.Update(user);

                if (result == 0)
                {
                    return Ok(new GenericResponse<UserProfile>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Operación fallida"
                    });
                }

                return Ok(new GenericResponse<UserProfile>
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.DeleteById(id);

                if (result == 0)
                {
                    return Ok(new GenericResponse<UserProfile>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Operación fallida"
                    });
                }
                else
                {
                    var findUser = _userService.GetById(id);
                    if (findUser is null)
                    {
                        return NotFound(new GenericResponse<UserProfile>
                        {
                            Status = (int)HttpStatusCode.NotFound,
                            Message = $"El id de usuario: {id} a desactivar no existe"
                        });
                    }
                }

                return Ok(new GenericResponse<UserProfile>
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
    }
}
