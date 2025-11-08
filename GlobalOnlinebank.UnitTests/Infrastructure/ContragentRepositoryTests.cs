using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Infrastructure.Data;
using GlobalOnlinebank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GlobalOnlinebank.UnitTests.Infrastructure
{
    public class ContragentRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public ContragentRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddProductToDatabase()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new ContragentRepository(context);
            var contragent = new Contragent("ТОО Рахат","ТОО Рахат","LLP Rahat",  "961545123258");

            // Act
            var result = await repository.AddAsync(contragent);

            // Assert
            Assert.NotEqual(0, result.Id);
            var savedContragent = await context.Contragents.FindAsync(result.Id);
            Assert.NotNull(savedContragent);
            Assert.Equal(contragent.RuName, savedContragent.RuName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new ContragentRepository(context);

            await context.Contragents.AddRangeAsync(
                new Contragent("ТОО Колбаса","ТОО Колбаса","LLP Kolbasa",  "961545123222"),
            new Contragent("ТОО Рахат","ТОО Рахат","LLP Rahat",  "961545123251")
            );
            await context.SaveChangesAsync();

            // Act
            var results = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, results.Count());
        }
    }
}
