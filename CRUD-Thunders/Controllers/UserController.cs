using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Application.IServices;
using CRUD_Thunders.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Thunders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("List")]
        public ActionResult<List<UserDTO>> ListUsers()
        {
            try
            {
                var listUser = _userService.GetUsers();

                return Ok(listUser);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpGet("ListById/{Id}")]
        public ActionResult<UserDTO> ListUser(Guid Id)
        {
            try
            {
                var User = _userService.GetUserById(Id);

                return Ok(User);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpPost("Post")]
        public ActionResult PostUser(User user)
        {
            try
            {
                _userService.PostUser(user);

                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpPut("Update")]

        public ActionResult UpdateUser(User user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{Id}")]
        public ActionResult DeleteUser(Guid Id)
        {
            try
            {
                 _userService.DeleteUser(Id);
                return Ok("Usuário deletado com sucesso!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }
    }
}
