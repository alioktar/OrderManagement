namespace OrderManagement.Core.Utilities.Response
{
    public class ErrorDataResponse<T> : DataResponse<T>
    {
        public ErrorDataResponse() : base(default, false) { }

        public ErrorDataResponse(T data) : base(data, false) { }

        public ErrorDataResponse(string message) : base(default, false, message) { }

        public ErrorDataResponse(T data, string message) : base(data, false, message) { }
    }
}
