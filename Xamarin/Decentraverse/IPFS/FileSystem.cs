using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ipfs.Api;
using Newtonsoft.Json;

namespace Decentraverse.IPFS
{
    public class FileSystem
    {
        public byte[] GetByHash(string hash)
        {
            var client = new IpfsClient("https://ipfs.infura.io:5001");
            var stream = client.FileSystem.ReadFileAsync(hash).Result;
            var ms = new MemoryStream();
            CopyStream(stream, ms);
            return ms.ToArray();
        }

        public T GetObjectByHash<T>(string hash)
        {
            var client = new IpfsClient("https://ipfs.infura.io:5001");
            var text = client.FileSystem.ReadAllTextAsync(hash).Result;
            var fileModel = JsonConvert.DeserializeObject<T>(text);
            return fileModel;
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
    }
}
