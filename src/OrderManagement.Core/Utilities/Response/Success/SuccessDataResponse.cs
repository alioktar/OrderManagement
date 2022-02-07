namespace OrderManagement.Core.Utilities.Response
{
    public class SuccessDataResponse<T> : DataResponse<T>
    {
        public SuccessDataResponse() : base(default, true) { }

        public SuccessDataResponse(T data) : base(data, true) { }

        public SuccessDataResponse(string message) : base(default, true, message) { }

        public SuccessDataResponse(T data, string message) : base(data, true, message) { }
    }
}
