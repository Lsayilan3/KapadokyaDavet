
using Business.Handlers.OrBabies.Commands;
using Business.Handlers.OrBabies.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OrBabies If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrBabiesController : BaseApiController
    {
        ///<summary>
        ///List OrBabies
        ///</summary>
        ///<remarks>OrBabies</remarks>
        ///<return>List OrBabies</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrBaby>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOrBabiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OrBabies</remarks>
        ///<return>OrBabies List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrBaby))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int orBabyId)
        {
            var result = await Mediator.Send(new GetOrBabyQuery { OrBabyId = orBabyId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OrBaby.
        /// </summary>
        /// <param name="createOrBaby"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrBabyCommand createOrBaby)
        {
            var result = await Mediator.Send(createOrBaby);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OrBaby.
        /// </summary>
        /// <param name="updateOrBaby"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrBabyCommand updateOrBaby)
        {
            var result = await Mediator.Send(updateOrBaby);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OrBaby.
        /// </summary>
        /// <param name="deleteOrBaby"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOrBabyCommand deleteOrBaby)
        {
            var result = await Mediator.Send(deleteOrBaby);
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
