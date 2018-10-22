using System;
using System.Collections.Generic;
using System.Linq;
using Decentraverse.Contracts;
using Decentraverse.Models;
using System.Threading.Tasks;

namespace Decentraverse.Services
{
    public class EthereumCardRepository : ICardRepository
    {
        protected ISolidityService solidityService;
        protected IDecentralisedFilesystemService decentralisedFilesystemService;

        public EthereumCardRepository(ISolidityService solidityService, IDecentralisedFilesystemService decentralisedFilesystemService)
        {
            this.solidityService = solidityService;
            this.decentralisedFilesystemService = decentralisedFilesystemService;
        }

        public async Task<Card> GetCard(string hash)
        {
            return null;
        }

        public async Task<IEnumerable<Card>> GetMyCards()
        {
            List<Card> cards = new List<Card>();

            int[] ids = solidityService.GetCardTokenIds().ToArray();
            foreach(int id in ids)
            {
                var owner = await solidityService.GetOwnerOf(id);
                if (owner.Equals(solidityService.MyAddress, StringComparison.OrdinalIgnoreCase))
                {
                    var ipfsHash = await solidityService.CardTokenIdToHash(id);
                    Card card = await decentralisedFilesystemService.DeserializeJSONObjectByHash<Card>(ipfsHash);
                    card.Token = id;
                    cards.Add(card);
                }
            }

            return cards;
        }

        public IEnumerable<int> GetCardTokenIds()
        {
            return solidityService.GetCardTokenIds();
        }
    }
}
