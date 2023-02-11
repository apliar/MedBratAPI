using MedBratAPI.Features.MedRecordFeatures.Commands;
using MedBratAPI.Features.MedRecordFeatures.Queries;
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
    public class MedRecordController : ControllerBase
    {
        private IMediator _mediator;

        public MedRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание журнала
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MedRecord), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateRecordCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");

            return CreatedAtAction(nameof(GetByPatientIdAndDoctorId), result);
        }
        /// <summary>
		///     Получение всех журналов по PatientId
		/// </summary>
        [HttpGet("ByPatientId/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<MedRecord>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByPatientId(int patientId)
        {
            var result = await _mediator.Send(new GetAllRecordsByPatientIdQuery { PatientId = patientId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение всех журналов по DoctorId
		/// </summary>
        [HttpGet("ByDoctorId/{doctorId}")]
        [ProducesResponseType(typeof(IEnumerable<MedRecord>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByDoctorId(int doctorId)
        {
            var result = await _mediator.Send(new GetAllRecordsByDoctorIdQuery { DoctorId = doctorId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Получение журнала по PatientId и DoctorId
		/// </summary>
        [HttpGet("ByPatientIdAndDoctorId/{patientId}&{doctorId}")]
        [ProducesResponseType(typeof(MedRecord), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByPatientIdAndDoctorId(int patientId, int doctorId)
        {
            var result = await _mediator.Send(new GetRecordByPatientIdAndDoctorIdQuery { PatientId = patientId, DoctorId = doctorId });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Добавление посещения в журнал
		/// </summary>
        [HttpPost("CreateVisit")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateVisit(CreateVisitForRecordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
		///     Удаление журнала по PatientId и DoctorId
		/// </summary>
        [HttpDelete("{patientId}&{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int patientId, int doctorId)
        {
            return Ok(await _mediator.Send(new DeleteRecordCommand { PatientId = patientId, DoctorId = doctorId }));
        }
    }
}
