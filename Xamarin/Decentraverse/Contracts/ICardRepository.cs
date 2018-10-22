using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decentraverse.Models;

namespace Decentraverse.Contracts
{
    public interface ICardRepository
    {
        Task<Card> GetCard(string hash);
        Task<IEnumerable<Card>> GetMyCards();
        IEnumerable<int> GetCardTokenIds();
    }
}
