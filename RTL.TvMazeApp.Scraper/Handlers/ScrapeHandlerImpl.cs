using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RTL.TvMazeApp.Domain.Interfaces;

namespace RTL.TvMazeApp.Scraper.Handlers
{
    public class ScrapeHandlerImpl : AsyncRequestHandler<ScrapeHandler>
    {
        private readonly IScrapeService _scrapeService;
        private readonly IShowRepository _showRepository;

        public ScrapeHandlerImpl(
            IScrapeService scrapeService,
            IShowRepository showRepository
            )
        {
            _scrapeService = scrapeService;
            _showRepository = showRepository;
        }

        protected override async Task Handle(ScrapeHandler request, CancellationToken cancellationToken)
        {
            var pageNumber = await GetNextPageNumber();
            var tvShows = await _scrapeService.GetShowsAsync(pageNumber);

            while (tvShows.Count > 0)
            {
                foreach (var show in tvShows)
                {
                    await _showRepository.CreateIfNotExistsAsync(show);
                }

                await _showRepository.SaveAsync();

                var nextPage = await GetNextPageNumber();
                tvShows = await _scrapeService.GetShowsAsync(nextPage);
            }
        }

        private async Task<int> GetNextPageNumber()
        {
            var show = await _showRepository.GetLastShowAsync();
            return (show?.ShowId ?? 0) / 250; // max of 250 per page
        }
    }
}
