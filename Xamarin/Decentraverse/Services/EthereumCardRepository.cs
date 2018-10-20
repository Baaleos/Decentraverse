using System;
using System.Collections.Generic;
using System.Linq;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Decentraverse.Services
{
    public class EthereumCardRepository : ICardRepository
    {
        private readonly string PrivateKey;

        public EthereumCardRepository(string privKey)
        {
            PrivateKey = privKey;
        }

        public Card GetCard(string hash)
        {

        }

        public IEnumerable<Card> GetMyCards()
        {
            
        }

        public IEnumerable<int> GetCardTokenIds()
        {
            return SolidityMethods.Common.GetCardTokenIds(PrivateKey);
        }
    }
}
