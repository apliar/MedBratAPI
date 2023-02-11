using MedBratAPI.Features.PatientFeatures.Commands;
using MedBratAPI.Features.PatientFeatures.Queries;
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
    public class PatientController : ControllerBase
    {
        private IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание пациента
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetAll), result);
        }
        /// <summary>
		///     Получение всех пациентов
		/// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPatientsQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение пациента по Id
		/// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPatientByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление пациента по Id
		/// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePatientByIdCommand { Id = id }));
        }
        /// <summary>
		///     Изменение пациента по Id
		/// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int id, UpdatePatientCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
