namespace OrderManagement.Core.Utilities.Exceptions
{
    public class NotFoundException : GeneralException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
