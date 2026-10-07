using JsonFlatFileDataStore;

namespace NobaCars.Infra
{
    // Wraps the JSON flat-file store. Only the repositories in this project touch it,
    // so the storage technology can be swapped without changing NobaCars.Core.
    internal sealed class Database(string filePath)
    {
        public DataStore Store { get; } = new DataStore(filePath);
    }
}
