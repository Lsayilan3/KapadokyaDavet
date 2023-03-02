
using Business.Handlers.OrNisans.Commands;
using Business.Handlers.OrNisans.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OrNisans If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrNisansController : BaseApiController
    {
        ///<summary>
        ///List OrNisans
        ///</summary>
        ///<remarks>OrNisans</remarks>
        ///<return>List OrNisans</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrNisan>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOrNisansQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OrNisans</remarks>
        ///<return>OrNisans List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrNisan))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int orNisanId)
        {
            var result = await Mediator.Send(new GetOrNisanQuery { OrNisanId = orNisanId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OrNisan.
        /// </summary>
        /// <param name="createOrNisan"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrNisanCommand createOrNisan)
        {
            var result = await Mediator.Send(createOrNisan);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OrNisan.
        /// </summary>
        /// <param name="updateOrNisan"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrNisanCommand updateOrNisan)
        {
            var result = await Mediator.Send(updateOrNisan);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OrNisan.
        /// </summary>
        /// <param name="deleteOrNisan"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOrNisanCommand deleteOrNisan)
        {
            var result = await Mediator.Send(deleteOrNisan);
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
