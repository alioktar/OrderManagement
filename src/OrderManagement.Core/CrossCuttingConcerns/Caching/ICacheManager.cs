namespace OrderManagement.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T? Get<T>(string key);
        object? Get(string key);
        void Add<T>(string key, T data, int duration = 60);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
