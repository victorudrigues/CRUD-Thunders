using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Application.IServices;
using CRUD_Thunders.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Thunders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        [HttpGet("List")]
        public ActionResult<List<ActivityDTO>> ListActivities()
        {
            try
            {
                var activityList = _activityService.GetActivities();
                return Ok(activityList);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpGet("ListById/{Id}")]
        public ActionResult<List<ActivityDTO>> ListActivity(Guid Id)
        {
            try
            {
                var activity = _activityService.GetActivityById(Id);

                return Ok(activity);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpPost("Post")]
        public ActionResult PostActivity(Activity activity)
        {
            try
            {
                if (!activity.IsValid())
                {
                    return BadRequest("O usuário relacionado não foi fornecido.");
                }

                _activityService.PostActivity(activity);

                return Ok("Atividade criada com sucesso");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpPut("Update")]

        public ActionResult UpdateActivity(Activity activity)
        {
            try
            {
                _activityService.UpdateActivity(activity);

                return Ok("Atividade Atualizada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{Id}")]
        public ActionResult DeleteActivity(Guid Id)
        {
            try
            {
                _activityService.DeleteActivity(Id);

                return Ok("Atividade deletada");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

    }
}
