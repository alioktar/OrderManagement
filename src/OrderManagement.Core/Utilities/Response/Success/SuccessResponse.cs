namespace OrderManagement.Core.Utilities.Response
{
    public class SuccessResponse : Response
    {
        public SuccessResponse(string message) : base(true, message) { }

        public SuccessResponse() : base(true) { }
    }
}
