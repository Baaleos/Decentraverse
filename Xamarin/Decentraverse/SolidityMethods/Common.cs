using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Decentraverse.SolidityMethods
{
    class Common
    {
        public static Contract GetContract(string privateKey, Web3 _web3 = null)
        {
            string abi = Resources.DecentraverseABI;
            string contractAddress = Resources.ContractAddress;
            var privKey = new Nethereum.Signer.EthECKey(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privKey);
            var web3 = _web3 ?? new Web3(account, "https://ropsten.infura.io");
            var contract = web3.Eth.GetContract(abi, contractAddress);
            return contract;
        }

        public static string GetOwnerOf(int tokenId, string privateKey)
        {
            var contract = GetContract(privateKey);
            var getOwnerOf = contract.GetFunction("ownerOf");
            var res =
                getOwnerOf.CallAsync<string>(tokenId).Result;
            return res;
        }

        public static int GetBalanceOf(string ownerAddress, string privateKey)
        {
            var contract = GetContract(privateKey);
            var getBalanceOf = contract.GetFunction("balanceOf");
            var res =
                getBalanceOf.CallAsync<int>(ownerAddress).Result;
            return res;
        }

        public void Approve(string to, int tokenId, string privateKey)
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var contract = SolidityMethods.Common.GetContract(privateKey);
            var createCelestialObject = contract.GetFunction("approve");
            var transactionHash =
                createCelestialObject.SendTransactionAndWaitForReceiptAsync(account.Address, new HexBigInteger(900000), new HexBigInteger(100000000000000000), null, to,tokenId).Result;

        }

        public void SafeTransferFrom(string from, string to, int tokenId, string privateKey)
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var contract = SolidityMethods.Common.GetContract(privateKey);
            var safeTransferFrom = contract.GetFunction("safeTransferFrom");
            var transactionHash =
                safeTransferFrom.SendTransactionAndWaitForReceiptAsync(account.Address, new HexBigInteger(900000), new HexBigInteger(100000000000000000), null, from,to, tokenId).Result;

        }

        public void PurchaseCard(string senderAddress, string privateKey)
        {
            var web3 = SolidityMethods.Common.GetWeb3(privateKey);
            var balance = web3.Eth.GetBalance.SendRequestAsync(senderAddress).Result;
            //Assert on the balance ? - needs to be at least 0.1 eth - this could be 1000000000000 or so wei though
            if (balance.Value < new HexBigInteger(100000000000000000).Value)
            {
                throw new Exception("Unable to purchase Celestial Object - not enough funds - require 0.1 eth");
            }
            var contract = SolidityMethods.Common.GetContract(privateKey, web3);
            var createCelestialObject = contract.GetFunction("createCelestialObject");
            var transactionHash =
                createCelestialObject.SendTransactionAndWaitForReceiptAsync(senderAddress, new HexBigInteger(900000), new HexBigInteger(100000000000000000), null, senderAddress).Result;

        }



        public static Web3 GetWeb3(string privateKey)
        {
            var privKey = new Nethereum.Signer.EthECKey(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privKey);

            var web3 = new Web3(account, "https://ropsten.infura.io");
            return web3;
        }

        public static IEnumerable<int> GetCardTokenIds(string privateKey)
        {
            List<int> listToReturn = new List<int>();
            var contract = GetContract(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var transferEvent = contract.GetEvent("Transfer");
            var filterAll = transferEvent.CreateFilterInput(new BlockParameter(4267026), null);
            var instances = transferEvent.GetAllChanges<TransferEvent>(filterAll);
            listToReturn = instances.Result.Where(ee => ee.Event.To.ToLower() == account.Address.ToLower()).Select(e => e.Event.TokenId)
                .ToList();
            return listToReturn;
        }

        public class TransferEvent
        {
            [Parameter("address", "from", 1, true)]
            public int From { get; set; }

            [Parameter("address", "to", 2, true)]
            public string To { get; set; }

            [Parameter("int", "tokenId", 3, true)]
            public int TokenId { get; set; }
        }


    }
}
