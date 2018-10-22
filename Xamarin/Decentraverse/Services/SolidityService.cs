using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Decentraverse.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Decentraverse.Services
{
    public class SolidityService : ISolidityService
    {
        public string MyAddress
        {
            get => MyAddress ?? (MyAddress = MyAccountAddress);
            private set => MyAddress = value;
        }

        public string EthereumServiceAddress => ethereumServiceResolver.URL;
        public string MyAccountAddress => MyEthereumAccount.Address;
        public string ContractAddress => Resources.ContractAddress;

        protected PrivateKeyResolver privateKeyResolver;
        protected string MyPrivateKey => privateKeyResolver.Key;

        protected EthereumServiceResolver ethereumServiceResolver;

        protected EthECKey MyEthereumPrivateKey
        {
            get => MyEthereumPrivateKey ?? (MyEthereumPrivateKey = new EthECKey(MyPrivateKey));
            private set => MyEthereumPrivateKey = value;
        }

        protected Account MyEthereumAccount
        {
            get => MyEthereumAccount ?? (MyEthereumAccount = new Account(MyEthereumPrivateKey));
            private set => MyEthereumAccount = value;
        }

        protected Web3 Web3
        {
            get => Web3 ?? (Web3 = new Web3(MyEthereumAccount, EthereumServiceAddress));
            private set => Web3 = value;
        }

        public SolidityService(PrivateKeyResolver privateKeyResolver, EthereumServiceResolver ethereumServiceResolver)
        {
            this.privateKeyResolver = privateKeyResolver;
            this.ethereumServiceResolver = ethereumServiceResolver;
        }

        public Contract GetContract()
        {
            string abi = Resources.DecentraverseABI;
            string contractAddress = Resources.ContractAddress;
            var contract = Web3.Eth.GetContract(abi, contractAddress);
            return contract;
        }

        public async Task<string> GetOwnerOf(int tokenId)
        {
            var contract = GetContract();
            var getOwnerOf = contract.GetFunction("ownerOf");
            var res =
                await getOwnerOf.CallAsync<string>(tokenId);
            return res;
        }

        public async Task<int> GetBalanceOf(string ownerAddress)
        {
            var contract = GetContract();
            var getBalanceOf = contract.GetFunction("balanceOf");
            var res =
                await getBalanceOf.CallAsync<int>(ownerAddress);
            return res;
        }

        public async Task Approve(string toAddress, int tokenId)
        { 
            var contract = GetContract();
            var createCelestialObject = contract.GetFunction("approve");
            var transactionHash =
                await createCelestialObject.SendTransactionAndWaitForReceiptAsync(MyEthereumAccount.Address, new HexBigInteger(900000), null, null, toAddress,tokenId);
        }

        public async Task ApproveForMe(int tokenId)
        {
            await Approve(MyAddress, tokenId);
        }

        public async Task SafeTransferFrom(string fromAddress, string toAddress, int tokenId)
        {
            var contract = GetContract();
            var safeTransferFrom = contract.GetFunction("safeTransferFrom");
            var transactionHash =
                await safeTransferFrom.SendTransactionAndWaitForReceiptAsync(MyEthereumAccount.Address, new HexBigInteger(900000), null, null, fromAddress,toAddress, tokenId);

        }

        public async Task SafeTransferFromMe(string toAddress, int tokenId)
        {
            await SafeTransferFrom(MyAddress, toAddress, tokenId);
        }

        public async Task PurchaseCardForMyself()
        {
            await PurchaseCard(MyAddress);
        }

        public async Task PurchaseCard(string senderAddress)
        {
            var balance = Web3.Eth.GetBalance.SendRequestAsync(senderAddress).Result;
            //Assert on the balance ? - needs to be at least 0.1 eth - this could be 1000000000000 or so wei though
            if (balance.Value < new HexBigInteger(100000000000000000).Value)
            {
                throw new Exception("Unable to purchase Celestial Object - not enough funds - require 0.1 eth");
            }
            var contract = GetContract();
            var createCelestialObject = contract.GetFunction("createCelestialObject");
            var transactionHash =
                await createCelestialObject.SendTransactionAndWaitForReceiptAsync(senderAddress, new HexBigInteger(900000), new HexBigInteger(100000000000000000), null, senderAddress);

        }

        public IEnumerable<int> GetCardTokenIds()
        {
            List<int> listToReturn = new List<int>();
            var contract = GetContract();
            var transferEvent = contract.GetEvent("Transfer");
            var filterAll = transferEvent.CreateFilterInput(new BlockParameter(4267026), null);
            var instances = transferEvent.GetAllChanges<TransferEvent>(filterAll);
            listToReturn = instances.Result.Where(ee => ee.Event.To.ToLower() == MyEthereumAccount.Address.ToLower()).Select(e => e.Event.TokenId)
                .ToList();
            return listToReturn;
        }

        public async Task<string> CardTokenIdToHash(int tokenId)
        {
            var contract = GetContract();
            var celestialObjs = contract.GetFunction("CelestialObjects");
            var res =
                await celestialObjs.CallAsync<string>(tokenId);
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
