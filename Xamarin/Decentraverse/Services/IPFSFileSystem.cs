using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Decentraverse.Contracts;
using Ipfs.Api;
using Newtonsoft.Json;

namespace Decentraverse.Services
{
    public class IPFSFileSystem : IDecentralisedFilesystemService
    {
        public async Task<Stream> GetByHash(string hash)
        {
            var client = new IpfsClient("https://ipfs.infura.io:5001");
            var stream = await client.FileSystem.ReadFileAsync(hash);
            var ms = new MemoryStream();
            CopyStream(stream, ms);
            return ms;
        }

        public async Task<T> DeserializeJSONObjectByHash<T>(string hash)
        {
            //var client = new IpfsClient("https://ipfs.infura.io:5001");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(CreateIPFSURL(hash));
                //var text = client.FileSystem.ReadAllTextAsync(hash).Result;
                var text = await response.Content.ReadAsStringAsync();
                var fileModel = JsonConvert.DeserializeObject<T>(text);
                return fileModel;
            }
        }

        public void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public string CreateIPFSURL(string hash)
        {
            return $"https://ipfs.infura.io/ipfs/{hash}";
        }
    }
}
