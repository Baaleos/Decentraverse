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

        public IEnumerable<int> GetCardTokenIds()
        {
            string privateKey = "";
            return SolidityMethods.Common.GetCardTokenIds(privateKey);
        }

        

       

        


    }
}
