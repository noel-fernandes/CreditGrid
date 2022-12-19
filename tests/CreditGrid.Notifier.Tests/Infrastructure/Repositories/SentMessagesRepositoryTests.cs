using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence.Repositories;
using FakeItEasy;

namespace CreditGrid.Notifier.Tests.Infrastructure.Repositories
{
    public class SentMessagesRepositoryTests
    {
        [Fact]
        public async Task SendAndAddMessageAsyncTest() 
        {
            // Arrange
            var context = DatabaseContext.Create();
            var fakeEmailClientService = A.Fake<IEmailClientService>();
            A.CallTo(() => fakeEmailClientService.SendEmailAsync(A<string>._, A<string>._, A<string>._, A<string>._, A<DateTimeOffset>._)).Returns("Hello");

            var recipientsName = "Name";
            var recipientsEmail = "a@b.com";
            var subject = "test subject";
            var messageBody = "test body";
            var sentOn = DateTimeOffset.UtcNow;

            var target = new SentMessagesRepository(context, fakeEmailClientService);

            // Act
            await target.SendAndAddMessageAsync(recipientsName, recipientsEmail, subject, messageBody, sentOn);
            await target.CompleteAsync();

            var result = await target.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            Assert.Equal(recipientsEmail, result.FirstOrDefault().RecipientsEmail);
        }
    }
}
