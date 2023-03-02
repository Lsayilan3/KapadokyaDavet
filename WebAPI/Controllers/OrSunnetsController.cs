
using Business.Handlers.OrSunnets.Commands;
using Business.Handlers.OrSunnets.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OrSunnets If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrSunnetsController : BaseApiController
    {
        ///<summary>
        ///List OrSunnets
        ///</summary>
        ///<remarks>OrSunnets</remarks>
        ///<return>List OrSunnets</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrSunnet>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOrSunnetsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OrSunnets</remarks>
        ///<return>OrSunnets List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrSunnet))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int orSunnetId)
        {
            var result = await Mediator.Send(new GetOrSunnetQuery { OrSunnetId = orSunnetId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OrSunnet.
        /// </summary>
        /// <param name="createOrSunnet"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrSunnetCommand createOrSunnet)
        {
            var result = await Mediator.Send(createOrSunnet);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OrSunnet.
        /// </summary>
        /// <param name="updateOrSunnet"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrSunnetCommand updateOrSunnet)
        {
            var result = await Mediator.Send(updateOrSunnet);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OrSunnet.
        /// </summary>
        /// <param name="deleteOrSunnet"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOrSunnetCommand deleteOrSunnet)
        {
            var result = await Mediator.Send(deleteOrSunnet);
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
