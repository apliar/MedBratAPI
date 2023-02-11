using MedBratAPI.Features.DoctorFeatures.Commands;
using MedBratAPI.Features.DoctorFeatures.Queries;
using MedBratAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedBratAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, moderator, curator")]
    public class DoctorController : ControllerBase
    {
        private IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание доктора
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetAll), result);
        }
        /// <summary>
		///     Получение всех докторов
		/// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDoctorsQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение доктора по Id
		/// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDoctorByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение докторов по Id клиники
		/// </summary>
        [HttpGet("ByClinicId/{clinicId}")]
        [ProducesResponseType(typeof(IEnumerable<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByClinicId(int clinicId)
        {
            var result = await _mediator.Send(new GetAllDoctorsByClinicIdQuery { ClinicId = clinicId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление доктора по Id
		/// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDoctorByIdCommand { Id = id }));
        }
        /// <summary>
		///     Изменение доктора по Id
		/// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, UpdateDoctorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
