using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decentraverse.Models;

namespace Decentraverse.Contracts
{
    public interface ICardRepository
    {
        Card GetCard(string hash);
        IEnumerable<Card> GetMyCards();
        string MyAddress { get; }

        IEnumerable<int> GetCardTokenIds();
    }
}
