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
using Decentraverse.SolidityMethods;
using Decentraverse.IPFS;
using System.Threading.Tasks;

namespace Decentraverse.Services
{
    public class EthereumCardRepository : ICardRepository
    {
        public string MyAddress {
            get {
                return myAddress ?? (myAddress = Solidity.GetAddress(PrivateKey));
            }
        }

        private string myAddress = null;
        private readonly PrivateKeyResolver PrivateKeyResolver;
        private string PrivateKey { get => PrivateKeyResolver.Key; }

        public EthereumCardRepository(PrivateKeyResolver privKeyResolver)
        {
            PrivateKeyResolver = privKeyResolver;
        }

        public Card GetCard(string hash)
        {
            return null;
        }

        public IEnumerable<Card> GetMyCards()
        {
            List<Card> cards = new List<Card>();


            Solidity.MyAddress = Solidity.GetAddress(PrivateKey);
            Solidity.MyPrivateKey = PrivateKey;
            //return new List<Card> {
            //    new Card("Jupiter", "QmadWQ2XCcWba4pySncC6jggijoQ5K11ZkxBQF98VUJRXb",
            //                            "Gas giant with red spot hundreds of kilometers in diameter.", Card.Rarity.UNIVERSAL),
            //    new Card("Black hole", "QmadWQ2XCcWba4pySncC6jggijoQ5K11ZkxBQF98VUJRXb",
            //             "A really big black hole.", Card.Rarity.SINGULAR)
            //};

            int[] ids = Solidity.GetCardTokenIds(PrivateKey).ToArray();
            List<System.Threading.Thread> Threads = new List<System.Threading.Thread>();
            foreach(int id in ids)
            {
                var _thread = new System.Threading.Thread(()=>{
                    var owner = Solidity.GetOwnerOf(id, PrivateKey);
                    if (owner.ToLower() == MyAddress.ToLower())
                    {
                        var ipfsHash = Solidity.CardTokenIdToHash(id, PrivateKey);
                        Card card = IPFSFileSystem.DeserializeJSONObjectByHash<Card>(ipfsHash);
                        card.Token = id;
                        cards.Add(card);
                    }
                });
                _thread.IsBackground = true;
                Threads.Add(_thread);
                _thread.Start();
            }
            while(Threads.Any(ee=> ee.IsAlive)){
                System.Threading.Thread.Sleep(100);
            }
            return cards;
        }

        public IEnumerable<int> GetCardTokenIds()
        {
            return Solidity.GetCardTokenIds(PrivateKey);
        }
    }
}
