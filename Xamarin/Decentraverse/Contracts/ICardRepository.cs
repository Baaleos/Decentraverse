using System;
using System.Collections.Generic;
using Decentraverse.Models;

namespace Decentraverse.Contracts
{
    public interface ICardRepository
    {
        Card GetCard(string hash);
        IEnumerable<Card> GetMyCards();
    }
}
