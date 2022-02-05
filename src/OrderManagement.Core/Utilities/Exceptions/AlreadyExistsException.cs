namespace OrderManagement.Core.Utilities.Exceptions
{
    public class AlreadyExistsException : GeneralException
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}
