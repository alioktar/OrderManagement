namespace OrderManagement.Core.Utilities.Response
{
    public class Response : IResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Response(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Response(bool success)
        {
            IsSuccess = success;
        }
    }
}
