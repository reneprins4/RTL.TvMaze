using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTL.TvMazeApp.Domain.Interfaces;
using RTL.TvMazeApp.Domain.Models;

namespace RTL.TvMazeApp.Scraper.Services
{
    public class ScrapeService : IScrapeService
    {
        public async Task<IEnumerable<Person>> GetPersonsAsync(int showId)
        {
            if (showId <= 0) throw new ArgumentOutOfRangeException(nameof(showId));

            var url = $"http://api.tvmaze.com/shows/{showId}/cast";
            var castResult = await GetJsonAsync(url);
            var json = JArray.Parse(castResult);
            return json
                .Select(p => p["person"]).Select(p => p.ToObject<Person>())
                .OrderByDescending(o => o.Birthday.HasValue) // Some values does not contain a date
                .ThenBy(o => o.Birthday)
                .ToList();
        }

        public async Task<List<Show>> GetShowsAsync(int pageId)
        {
            if (pageId < 0) throw new ArgumentOutOfRangeException(nameof(pageId));

            var url = $"http://api.tvmaze.com/shows?page={pageId}";
            var showResult = await GetJsonAsync(url);
            var showList = JsonConvert.DeserializeObject<Show[]>(showResult);
            var shows = new List<Show>();

            foreach (var show in showList)
            {
                var cast = await GetPersonsAsync(show.ShowId);
                show.Cast = cast;
                shows.Add(show);
            }
            return shows;
        }

        private async Task<string> GetJsonAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            using (var client = new HttpClient())
            using (var req = new HttpRequestMessage(HttpMethod.Get, url))
            using (var res = await client.SendAsync(req))
            {
                var result = await res.Content.ReadAsStringAsync();
                if (result == null) throw new ArgumentNullException(nameof(result));

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    return result;
                else
                    return null;
            }
        }
    }
}
