using System;
using System.Collections.Generic;
using Decentraverse.Contracts;
using Decentraverse.Models;

namespace Decentraverse.Services
{
    public class CardRepository : ICardRepository
    {
        public Card GetCard(string hash)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetMyCards()
        {
            throw new NotImplementedException();
        }
    }
}
