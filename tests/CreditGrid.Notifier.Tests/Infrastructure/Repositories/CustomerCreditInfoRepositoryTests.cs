using CreditGrid.Notifier.Domain.Exceptions;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using CreditGrid.Notifier.Infrastructure.Persistence.Repositories;

namespace CreditGrid.Notifier.Tests.Infrastructure.Repositories
{
    public class CustomerCreditInfoRepositoryTests
    {

        [Fact]
        public async Task GetAllAsync_WhenNone_ReturnsZeroCollection()
        {
            // Arrange
            var context = DatabaseContext.Create();
            var target = new CustomerCreditInfoRepository(context);

            // Act
            var result = await target.GetAllAsync();

            // Assert
            Assert.False(result.Any());
        }

        [Fact]
        public async Task GetAllAsync_WhenExists_ReturnsAll()
        {
            // Arrange
            var ccInfo = new CustomerCreditInformation()
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@doe.com",
                CreditNumber = "132456789",
                AmountDue = 1234.76M,
                DueDate = DateTimeOffset.UtcNow
            };
            var context = DatabaseContext.Create();
            var target = new CustomerCreditInfoRepository(context);
            await target.AddEntityAsync(ccInfo);
            await target.CompleteAsync();

            // Act
            var result = await target.GetAllAsync();

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetByCreditNumber_WhenNone_ThrowsCustomerCreditInformationNotFoundException()
        {
            // Arrange
            var creditNumber = "654365483354";
            var context = DatabaseContext.Create();
            var target = new CustomerCreditInfoRepository(context);

            // Act
            async Task Act()
            {
                _ = await target.GetByCreditNumberAsync(creditNumber);
            }

            // Assert
            await Assert.ThrowsAsync<CustomerCreditInformationNotFoundException>(() => Act());
        }

        [Fact]
        public async Task GetByCreditNumber_WhenExists_ReturnsCustomerCreditInformation() 
        {
            // Arrange
            var creditNumber1 = "654365483354";
            var creditNumber2 = "654213879525";

            var ccInfo1 = new CustomerCreditInformation()
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@doe.com",
                CreditNumber = creditNumber1,
                AmountDue = 1234.76M,
                DueDate = DateTimeOffset.UtcNow
            };

            var ccInfo2 = new CustomerCreditInformation()
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                Email = "jane@doe.com",
                CreditNumber = creditNumber2,
                AmountDue = 1234.76M,
                DueDate = DateTimeOffset.UtcNow
            };

            var context = DatabaseContext.Create();
            var target = new CustomerCreditInfoRepository(context);
            await target.AddEntityAsync(ccInfo1);
            await target.AddEntityAsync(ccInfo2);
            await target.CompleteAsync();

            // Act
            var result = await target.GetByCreditNumberAsync(creditNumber2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(creditNumber2, result.CreditNumber);
            Assert.Equal(ccInfo2, result);
        }

        [Fact]
        public async Task AddCustomerCreditInformationAsync_AddsNewCustomerCreditInformationInDb()
        {
            var id = Guid.NewGuid();
            var creditNumber = "654213879525";

            var ccInfo = new CustomerCreditInformation()
            {
                Id = id,
                Name = "John Doe",
                Email = "john@doe.com",
                CreditNumber = creditNumber,
                AmountDue = 1234.76M,
                DueDate = DateTimeOffset.UtcNow
            };

            var context = DatabaseContext.Create();
            var target = new CustomerCreditInfoRepository(context);

            // Act
            await target.AddEntityAsync(ccInfo);
            await target.CompleteAsync();
            var results = await target.GetAllAsync();
            var result = results.FirstOrDefault(t => t.Id == id);

            // Assert
            Assert.True(results.Any());
            Assert.NotNull(result);
            Assert.Equal(ccInfo, result);
        }
    }
}
