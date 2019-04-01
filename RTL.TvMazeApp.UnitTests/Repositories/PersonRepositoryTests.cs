using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RTL.TvMazeApp.Domain.Models;
using RTL.TvMazeApp.Infrastructure.Contexts;
using RTL.TvMazeApp.Infrastructure.Repositories;

namespace RTL.TvMazeApp.UnitTests.Repositories
{
    class PersonRepositoryTests
    {
        DbContextOptions<TvMazeContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TvMazeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Test]
        public void CreateAsync_ShowIsNull()
        {
            // Given
            Person person = null;

            using (var context = new TvMazeContext(options))
            {
                var castRepository = new PersonRepository(context);

                Assert.ThrowsAsync<ArgumentNullException>(async () => await castRepository.CreateIfNotExistsAsync(person));
            }
        }
    }
}
