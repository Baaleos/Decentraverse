using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ipfs.Api;
using Newtonsoft.Json;

namespace Decentraverse.IPFS
{
    public static class IPFSFileSystem
    {
        public static Stream GetByHash(string hash)
        {
            var client = new IpfsClient("https://ipfs.infura.io:5001");
            var stream = client.FileSystem.ReadFileAsync(hash).Result;
            var ms = new MemoryStream();
            CopyStream(stream, ms);
            return ms;
        }

        public static T DeserializeJSONObjectByHash<T>(string hash)
        {
            //var client = new IpfsClient("https://ipfs.infura.io:5001");

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(CreateIPFSURL(hash)).Result;
                //var text = client.FileSystem.ReadAllTextAsync(hash).Result;
                var text = response.Content.ReadAsStringAsync().Result;
                var fileModel = JsonConvert.DeserializeObject<T>(text);
                return fileModel;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static string CreateIPFSURL(string hash)
        {
            return $"https://ipfs.infura.io/ipfs/{hash}";
        }
    }
}
