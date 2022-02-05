namespace OrderManagement.Core.Utilities.Response
{
    public interface IDataResponse<out T> : IResponse
    {
        T? Data { get; }
    }
}
