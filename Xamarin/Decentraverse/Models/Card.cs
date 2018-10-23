using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Decentraverse.IPFS;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Decentraverse.Models
{
    public class Card
    {
        public enum Rarity
        {
            /// <summary>
            /// Tier 1
            /// </summary>
            UNIVERSAL = 0,

            /// <summary>
            /// Tier 2
            /// </summary>
            COMMON = 1,

            /// <summary>
            /// Tier 3
            /// </summary>
            HEAVENLY = 2,

            /// <summary>
            /// Tier 4
            /// </summary>
            SINGULAR = 3
        }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Fact { get; protected set; }

        [JsonProperty(PropertyName = "Rarity")]
        public Rarity CardRarity { get; protected set; }

        [JsonProperty(PropertyName = "Stats")]
        public Dictionary<string, string> Stats;

        [JsonIgnore]
        public int Token { get; set; }

        [JsonProperty(PropertyName = "Image")]
        public string ImageHash { get; protected set; }


        private ImageSource _image;
        public ImageSource GetImage()
        {
            if (_image == null)
            {
                //Stream stream = await IPFSFileSystem.GetByHash(ImageToken);
                //_image = ImageSource.FromStream(() => stream);
                _image = ImageSource.FromUri(new Uri(IPFSFileSystem.CreateIPFSURL(ImageHash)));
            }
            return _image;
        }

        public Card()
        { }

        public Card(string name, string imageToken, string fact, Rarity rarity)
        {
            Name = name;
            ImageHash = imageToken;
            Fact = fact;
            CardRarity = rarity;
        }
    }
}
