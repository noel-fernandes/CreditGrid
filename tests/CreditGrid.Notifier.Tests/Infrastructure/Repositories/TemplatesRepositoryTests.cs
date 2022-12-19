using CreditGrid.Notifier.Domain.Enums;
using CreditGrid.Notifier.Domain.Exceptions;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using CreditGrid.Notifier.Infrastructure.Persistence.Repositories;

namespace CreditGrid.Notifier.Tests.Infrastructure.Repositories
{
    public class TemplatesRepositoryTests
    {

        [Fact]
        public async Task GetAllAsync_WhenNone_ReturnsZeroCollection()
        {
            // Arrange
            var context = DatabaseContext.Create();
            var target = new TemplatesRepository(context);

            // Act
            var result = await target.GetAllAsync();

            // Assert
            Assert.False(result.Any());
        }

        [Fact]
        public async Task GetAllAsync_WhenExists_ReturnsTemplatesCollection()
        {
            // Arrange
            var templateId = Guid.NewGuid();
            var templateType = TemplateType.Reminder;

            var template = new Template()
            {
                Id = templateId,
                TemplateType = templateType,
                Subject = "Test Subject",
                Content = "Test Content"
            };

            var context = DatabaseContext.Create();
            var target = new TemplatesRepository(context);
            await target.AddEntityAsync(template);
            await target.CompleteAsync();

            // Act
            var result = await target.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            Assert.Equal(template, result.FirstOrDefault());
        }

        [Fact]
        public async Task GetByTypeAsync_WhenNone_ThrowsTemplateNotFoundException()
        {
            // Arrange
            var context = DatabaseContext.Create();
            var target = new TemplatesRepository(context);

            // Act
            async Task Act()
            {
                _ = await target.GetByTypeAsync(TemplateType.OverdueReminder);
            }

            // Assert
            await Assert.ThrowsAsync<TemplateNotFoundException>(() => Act());
        }

        [Fact]
        public async Task GetByTypeAsync_WhenExists_ReturnsTemplateOfType()
        {
            // Arrange
            var template1Type = TemplateType.Reminder;
            var template2Type = TemplateType.OverdueReminder;

            var template1 = new Template()
            {
                Id = Guid.NewGuid(),
                TemplateType = template1Type,
                Subject = "Test Subject",
                Content = "Test Content"
            };

            var template2 = new Template()
            {
                Id = Guid.NewGuid(),
                TemplateType = template2Type,
                Subject = "Test Subject",
                Content = "Test Content"
            };

            var context = DatabaseContext.Create();
            var target = new TemplatesRepository(context);
            await target.AddEntityAsync(template1);
            await target.AddEntityAsync(template2);
            await target.CompleteAsync();

            // Act
            var result = await target.GetByTypeAsync(TemplateType.OverdueReminder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TemplateType.OverdueReminder, result.TemplateType);
        }

        [Fact]
        public async Task AddTemplateAsync_AddsNewTemplateInDb()
        {
            // Arrange
            var templateId = Guid.NewGuid();
            var templateType = TemplateType.Reminder;

            var template = new Template()
            {
                Id = templateId,
                TemplateType = templateType,
                Subject = "Test Subject",
                Content = "Test Content"
            };

            var context = DatabaseContext.Create();
            var target = new TemplatesRepository(context);

            // Act
            await target.AddEntityAsync(template);
            await target.CompleteAsync();
            var results = await target.GetAllAsync();
            var result = results.FirstOrDefault(t => t.Id == templateId);

            // Assert
            Assert.True(results.Any());
            Assert.NotNull(result);
            Assert.Equal(template, result);
        }
    }
}
