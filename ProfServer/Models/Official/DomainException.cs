namespace ProfServer.Models.Official
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} with identifier '{key}' was not found.")
        {
        }
    }

    public class ValidationException : DomainException
    {
        public ValidationException(string message)
            : base(message)
        {
        }
    }

    public class ConflictException : DomainException
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    }

    public class AuthException : DomainException
    {
        public AuthException()
            : base("Authentication failed. Invalid credentials.")
        {
        }
    }
}
