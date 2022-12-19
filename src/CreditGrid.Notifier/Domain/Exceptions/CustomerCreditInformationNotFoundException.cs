using System.Runtime.Serialization;

namespace CreditGrid.Notifier.Domain.Exceptions
{
    [Serializable]
    public class CustomerCreditInformationNotFoundException : Exception
    {
        public CustomerCreditInformationNotFoundException()
        {
        }

        public CustomerCreditInformationNotFoundException(string? message) : base(message)
        {
        }

        public CustomerCreditInformationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CustomerCreditInformationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
