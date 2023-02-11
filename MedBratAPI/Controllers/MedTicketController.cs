using MedBratAPI.Features.MedTicketFeatures.Commands;
using MedBratAPI.Features.MedTicketFeatures.Queries;
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
    public class MedTicketController : ControllerBase
    {
        private IMediator _mediator;

        public MedTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание талона
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MedTicket), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetAllByPatientId), result);
        }
        /// <summary>
		///     Получение всех талонов по PatientId
		/// </summary>
        [HttpGet("ByPatientId/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<MedTicket>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByPatientId(int patientId)
        {
            var result = await _mediator.Send(new GetAllTicketsByPatientIdQuery { PatientId = patientId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение всех талонов по DoctorId
		/// </summary>
        [HttpGet("ByDoctorId/{doctorId}")]
        [ProducesResponseType(typeof(IEnumerable<MedTicket>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByDoctorId(int doctorId)
        {
            var result = await _mediator.Send(new GetAllTicketsByDoctorIdQuery { DoctorId = doctorId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление талона по Id
		/// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTicketByIdCommand { Id = id }));
        }
    }
}
