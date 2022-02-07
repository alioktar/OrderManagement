namespace OrderManagement.Core.Utilities.Response
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message) : base(false, message) { }

        public ErrorResponse() : base(false) { }
    }
}
