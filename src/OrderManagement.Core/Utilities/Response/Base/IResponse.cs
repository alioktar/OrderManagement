namespace OrderManagement.Core.Utilities.Response
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
