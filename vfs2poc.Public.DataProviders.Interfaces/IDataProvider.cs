using vfs2poc.Configuration.Model;

namespace vfs2poc.Public.DataProviders.Interfaces
{
    public interface IDataProvider<T>
    {
        T GetData(EntityView view);
    }
}
