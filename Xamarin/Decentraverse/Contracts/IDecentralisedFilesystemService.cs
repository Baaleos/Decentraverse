using System.IO;
using System.Threading.Tasks;

namespace Decentraverse.Contracts
{
    public interface IDecentralisedFilesystemService
    {
        void CopyStream(Stream input, Stream output);
        string CreateIPFSURL(string hash);
        Task<T> DeserializeJSONObjectByHash<T>(string hash);
        Task<Stream> GetByHash(string hash);
    }
}