using MedBratAPI.Features.ScheduleFeatures.Commands;
using MedBratAPI.Features.ScheduleFeatures.Queries;
using MedBratAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MedBratAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, moderator, curator")]
    public class ScheduleController : ControllerBase
    {
        private IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание расписания
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Schedule), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateScheduleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetById), result);
        }
        /// <summary>
		///     Получение расписания по DoctorId
		/// </summary>
        [HttpGet("{doctorId}")]
        [ProducesResponseType(typeof(Schedule), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int doctorId)
        {
            var result = await _mediator.Send(new GetScheduleByDoctorIdQuery { DoctorId = doctorId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление расписания по DoctorId
		/// </summary>
        [HttpDelete("{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int doctorId)
        {
            return Ok(await _mediator.Send(new DeleteScheduleByDoctorIdCommand { DoctorId = doctorId }));
        }
        /// <summary>
		///     Изменение расписания по DoctorId
		/// </summary>
        [HttpPut("{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int doctorId, UpdateScheduleCommand command)
        {
            if (doctorId != command.DoctorId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
