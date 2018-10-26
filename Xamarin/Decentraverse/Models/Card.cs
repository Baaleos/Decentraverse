using System;
using System.Collections.Generic;
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

        [JsonIgnore]
        public ImageSource ImageSource { get; protected set; }

        public Card(string name, string imageToken, string fact, Rarity rarity, ImageSource source)
        {
            Name = name;
            ImageHash = imageToken;
            Fact = fact;
            CardRarity = rarity;
            ImageSource = source;
        }

        public Card()
        { }
    }
}
