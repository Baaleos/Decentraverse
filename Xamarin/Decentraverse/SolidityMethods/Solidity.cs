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
    public static class Solidity
    {
        public static string MyAddress {
            get; set;
        }

        public static string MyPrivateKey {
            get; set;
        }

        public static string GetAddress(string privateKey, Web3 web3 = null)
        {
            string abi = Resources.DecentraverseABI;
            string contractAddress = Resources.ContractAddress;
            var privKey = new Nethereum.Signer.EthECKey(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privKey);
            return account.Address;
        }

        public static Contract GetContract(string privateKey, Web3 _web3 = null)
        {
            string abi = Resources.DecentraverseABI;
            string contractAddress = Resources.ContractAddress;
            var privKey = new Nethereum.Signer.EthECKey(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privKey);
            var web3 = _web3 ?? new Web3(account, "https://ropsten.infura.io/v3/2105932667224bc4a18e97688178ad03");
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

        public static void Approve(string toAddress, int tokenId, string privateKey)
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var contract = SolidityMethods.Solidity.GetContract(privateKey);
            var createCelestialObject = contract.GetFunction("approve");
            var transactionHash =
                createCelestialObject.SendTransactionAndWaitForReceiptAsync(account.Address, new HexBigInteger(900000), null, null, toAddress,tokenId).Result;

        }

        public static void ApproveForMe(int tokenId)
        {
            Approve(MyAddress, tokenId, MyPrivateKey);
        }

        public static void SafeTransferFrom(string fromAddress, string toAddress, int tokenId, string privateKey)
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var contract = SolidityMethods.Solidity.GetContract(privateKey);
            var safeTransferFrom = contract.GetFunction("safeTransferFrom");
            var transactionHash =
                safeTransferFrom.SendTransactionAndWaitForReceiptAsync(account.Address, new HexBigInteger(900000), null, null, fromAddress,toAddress, tokenId).Result;

        }

        public static void SafeTransferFromMe(string toAddress, int tokenId)
        {
            SafeTransferFrom(MyAddress, toAddress, tokenId, MyPrivateKey);
        }

        public static void PurchaseCard(string senderAddress, string privateKey)
        {
            var web3 = SolidityMethods.Solidity.GetWeb3(privateKey);
            var balance = web3.Eth.GetBalance.SendRequestAsync(senderAddress).Result;
            //Assert on the balance ? - needs to be at least 0.1 eth - this could be 1000000000000 or so wei though
            if (balance.Value < new HexBigInteger(100000000000000000).Value)
            {
                throw new Exception("Unable to purchase Celestial Object - not enough funds - require 0.1 eth");
            }
            var contract = SolidityMethods.Solidity.GetContract(privateKey, web3);
            var createCelestialObject = contract.GetFunction("createCelestialObject");
            var transactionHash =
                createCelestialObject.SendTransactionAndWaitForReceiptAsync(senderAddress, new HexBigInteger(900000), new HexBigInteger(100000000000000000), null, senderAddress).Result;

        }



        public static Web3 GetWeb3(string privateKey)
        {
            var privKey = new Nethereum.Signer.EthECKey(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privKey);

            var web3 = new Web3(account, "https://ropsten.infura.io/v3/2105932667224bc4a18e97688178ad03");
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

        public static string CardTokenIdToHash(int tokenId, string privateKey)
        {
            
            var contract = GetContract(privateKey);
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var celestialObjs = contract.GetFunction("CelestialObjects");
            var res =
                celestialObjs.CallAsync<string>(tokenId).Result;
            return res;

        }



        public class TransferEvent
        {
            [Parameter("address", "from", 1, true)]
            public string From { get; set; }

            [Parameter("address", "to", 2, true)]
            public string To { get; set; }

            [Parameter("int", "tokenId", 3, true)]
            public int TokenId { get; set; }
        }


    }
}
