using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decentraverse.Contracts
{
    public interface ISolidityService
    {
        string MyAddress { get; }

        Task Approve(string toAddress, int tokenId);
        Task ApproveForMe(int tokenId);
        Task<string> CardTokenIdToHash(int tokenId);
        Task<int> GetBalanceOf(string ownerAddress);
        IEnumerable<int> GetCardTokenIds();
        Task<string> GetOwnerOf(int tokenId);
        Task PurchaseCard(string senderAddress);
        Task SafeTransferFrom(string fromAddress, string toAddress, int tokenId);
        Task SafeTransferFromMe(string toAddress, int tokenId);
    }
}