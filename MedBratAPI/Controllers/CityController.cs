using MedBratAPI.Features.CityFeatures.Queries;
using MedBratAPI.Features.CityFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MedBratAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, moderator")]
    public class CityController : ControllerBase
    {
        private IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
		///     Создание города
		/// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(City), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCityCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Already exists");
            
            return CreatedAtAction(nameof(GetAll), result);
        }
        /// <summary>
		///     Получение всех городов
		/// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCitiesQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
		///     Удаление города по Id
		/// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCityByIdCommand { Id = id }));
        }
    }
}
