using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Public.DataProviders.Interfaces
{
    public interface IDataProvider<T>
    {
        T GetData(IView view);
    }
}
