using System;
using System.Collections.Generic;
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

        public string ImageURL { get; protected set; }
        public string CardName { get; protected set; }
        public string Fact { get; protected set; }
        public Rarity CardRarity { get; protected set; }
        public readonly IEnumerable<Statistic> Stats;

        public Card(string name, string imageURL, string fact, Rarity rarity, IEnumerable<Statistic> stats = null)
        {
            CardName = name;
            ImageURL = imageURL;
            Fact = fact;
            CardRarity = rarity;
            if (stats == null)
                stats = new List<Statistic>();
            Stats = new List<Statistic>(stats);
        }
    }
}
