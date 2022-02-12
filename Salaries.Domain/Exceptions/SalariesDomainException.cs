using System;

namespace Salaries.Domain.Exceptions
{
    public class SalariesDomainException: Exception
    {
        public SalariesDomainException()
        { }

        public SalariesDomainException(string message)
            : base(message)
        { }

        public SalariesDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
