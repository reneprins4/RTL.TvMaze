using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTL.TvMazeApp.Domain.Interfaces;

namespace RTL.TvMazeApp.Controllers
{
    [Route("api/show")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _showRepository;

        public ShowController(IShowRepository showRepository)
        {
            this._showRepository = showRepository;
        }

        // GET: api/show/7
        [HttpGet("{showId}")]
        public async Task<IActionResult> GetShow(int showId)
        {
            try
            {
                var show = await _showRepository.GetShowAsync(showId);

                return Ok(show);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}