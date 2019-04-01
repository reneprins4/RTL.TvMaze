using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTL.TvMazeApp.Domain.Interfaces;
using RTL.TvMazeApp.Domain.Models;

namespace RTL.TvMazeApp.Controllers
{
    [Route("api/scrape")]
    [ApiController]
    public class ScrapeController : ControllerBase
    {
        private readonly IScrapeService _scrapeService;
        private readonly IShowRepository _showRepository;
        private readonly IPersonRepository _personRepository;

        public ScrapeController(IScrapeService scrapeService, 
            IShowRepository showRepository, 
            IPersonRepository personRepository)
        {
            _scrapeService = scrapeService;
            _showRepository = showRepository;
            _personRepository = personRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Scrape()
        {
            var shows = await _scrapeService.GetShowsAsync(0);
            while (shows.Count > 0)
            {
                foreach (var show in shows)
                {
                    await _showRepository.CreateIfNotExistsAsync(show);
                    await _showRepository.SaveAsync();

                    foreach (var person in show.Cast) await _personRepository.CreateIfNotExistsAsync(person);
                    await _personRepository.SaveAsync();
                }

                var nextPage = (await _showRepository.GetLastShowAsync())?.ShowId ?? 0 / 250;
                shows = await _scrapeService.GetShowsAsync(nextPage);
            }

            return Ok();
        }
    }
}