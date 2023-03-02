
using Business.Handlers.SpotCategoryies.Commands;
using Business.Handlers.SpotCategoryies.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// SpotCategoryies If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SpotCategoryiesController : BaseApiController
    {
        ///<summary>
        ///List SpotCategoryies
        ///</summary>
        ///<remarks>SpotCategoryies</remarks>
        ///<return>List SpotCategoryies</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SpotCategoryy>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetSpotCategoryiesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>SpotCategoryies</remarks>
        ///<return>SpotCategoryies List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpotCategoryy))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        [AllowAnonymous]

        public async Task<IActionResult> GetById(int spotCategoryyId)
        {
            var result = await Mediator.Send(new GetSpotCategoryyQuery { SpotCategoryyId = spotCategoryyId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add SpotCategoryy.
        /// </summary>
        /// <param name="createSpotCategoryy"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSpotCategoryyCommand createSpotCategoryy)
        {
            var result = await Mediator.Send(createSpotCategoryy);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update SpotCategoryy.
        /// </summary>
        /// <param name="updateSpotCategoryy"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSpotCategoryyCommand updateSpotCategoryy)
        {
            var result = await Mediator.Send(updateSpotCategoryy);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete SpotCategoryy.
        /// </summary>
        /// <param name="deleteSpotCategoryy"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSpotCategoryyCommand deleteSpotCategoryy)
        {
            var result = await Mediator.Send(deleteSpotCategoryy);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
