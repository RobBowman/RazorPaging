namespace Utility.Error.Application.Interfaces
{
    /// <summary>
    /// ICacheProvider Interface.
    /// </summary>
    public interface ICacheProvider
    {
        void Add(string key, object obj);
        object Get(string key);
    }
}
