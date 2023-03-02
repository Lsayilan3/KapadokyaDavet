
using Business.Handlers.OrPikniks.Commands;
using Business.Handlers.OrPikniks.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OrPikniks If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrPikniksController : BaseApiController
    {
        ///<summary>
        ///List OrPikniks
        ///</summary>
        ///<remarks>OrPikniks</remarks>
        ///<return>List OrPikniks</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrPiknik>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOrPikniksQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OrPikniks</remarks>
        ///<return>OrPikniks List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrPiknik))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int orPiknikId)
        {
            var result = await Mediator.Send(new GetOrPiknikQuery { OrPiknikId = orPiknikId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OrPiknik.
        /// </summary>
        /// <param name="createOrPiknik"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrPiknikCommand createOrPiknik)
        {
            var result = await Mediator.Send(createOrPiknik);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OrPiknik.
        /// </summary>
        /// <param name="updateOrPiknik"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrPiknikCommand updateOrPiknik)
        {
            var result = await Mediator.Send(updateOrPiknik);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OrPiknik.
        /// </summary>
        /// <param name="deleteOrPiknik"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOrPiknikCommand deleteOrPiknik)
        {
            var result = await Mediator.Send(deleteOrPiknik);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addPhoto")]
        public async Task<IActionResult> AddPhoto([FromForm] AddPhotoCommad addPhoto)
        {

            var result = await Mediator.Send(addPhoto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
