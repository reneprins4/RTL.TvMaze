using System.Threading.Tasks;
using MediatR;
using RTL.TvMazeApp.Scraper.Handlers;

namespace RTL.TvMazeApp.Scraper.Jobs
{
    public class ScrapeJob
    {
        private readonly IMediator _mediator;

        public ScrapeJob(IMediator mediator) => this._mediator = mediator;

        public Task ExecuteAsync()
        {
            return _mediator.Send(new ScrapeHandler());
        }
    }
}
