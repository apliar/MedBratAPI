using MedBratAPI.Features.CuratorFeatures.Commands;
using MedBratAPI.Features.CuratorFeatures.Queries;
using MedBratAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedBratAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, moderator")]
    public class CuratorController : ControllerBase
    {
        private IMediator _mediator;

        public CuratorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание куратора
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Curator), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateCuratorCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetAll), result);
        }
        /// <summary>
		///     Получение всех кураторов
		/// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Curator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCuratorsQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение куратора по Id
		/// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Curator), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCuratorByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение кураторов по Id клиники
		/// </summary>
        [HttpGet("ByClinicId/{clinicId}")]
        [ProducesResponseType(typeof(IEnumerable<Curator>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByClinicId(int clinicId)
        {
            var result = await _mediator.Send(new GetAllCuratorsByClinicIdQuery { ClinicId = clinicId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление куратора по Id
		/// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCuratorByIdCommand { Id = id }));
        }
        /// <summary>
		///     Изменение куратора по Id
		/// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int id, UpdateCuratorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
