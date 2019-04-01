using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RTL.TvMazeApp.Domain.Models;
using RTL.TvMazeApp.Infrastructure.Contexts;
using RTL.TvMazeApp.Infrastructure.Repositories;

namespace RTL.TvMazeApp.UnitTests.Repositories
{
    class ShowRepositoryTests
    {
        // seed list of shows
        private List<Show> showList;

        DbContextOptions<TvMazeContext> options;

        [SetUp]
        public void Setup()
        {

            showList = new List<Show>
            {
                new Show { Id = 1, ShowId = 1, Name = "#1 show", Cast = new List<Person>
                {
                     new Person { Id = 1, Name = "#1 Cast", Birthday = DateTime.Parse("03-01-1980") }
                } },
                new Show { Id = 2, ShowId = 100, Name = "#2 show", Cast = new List<Person>
                {
                     new Person { Id = 2, Name = "#5 Cast", Birthday = DateTime.Parse("03-01-1980") },
                     new Person { Id = 3, Name = "#9 Cast", Birthday = DateTime.Parse("03-05-1986") }
                } },
                new Show { Id = 3, ShowId = 13457, Name = "#3 show", Cast = new List<Person>
                {
                     new Person { Id = 4, Name = "#10 Cast", Birthday = DateTime.Parse("10-01-1950") }
                } }
            };

            options = new DbContextOptionsBuilder<TvMazeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new TvMazeContext(options))
            {
                context.AddRange(showList);
                context.SaveChanges();
            }
        }

        [Test]
        public void CreateAsync_ShowIsNull()
        {
            // Given
            Show show = null;

            using (var context = new TvMazeContext(options))
            {
                var showRepository = new ShowRepository(context);

                Assert.ThrowsAsync<ArgumentNullException>(async () => await showRepository.CreateIfNotExistsAsync(show));
            }
        }

        [Test]
        public async Task CreateAsync_ItemIsAdded()
        {
            // Arrange
            var show = new Show()
            {
                Id = 101,
                Name = "abc"
            };

            // Act
            using (var context = new TvMazeContext(options))
            {
                var showRepository = new ShowRepository(context);
                await showRepository.CreateIfNotExistsAsync(show);
                await showRepository.SaveAsync();
            }

            // Assert
            using (var context = new TvMazeContext(options))
            {
                Assert.That(await context.Shows.CountAsync(), Is.EqualTo(4));
                Assert.That((await context.Shows.SingleAsync(q => q.Id == 101)).Name, Is.EqualTo("abc"));
            }
        }
    }
}
