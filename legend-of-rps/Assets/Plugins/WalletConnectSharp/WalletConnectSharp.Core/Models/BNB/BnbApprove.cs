using Newtonsoft.Json;
using WalletConnectSharp.Core.Models.Ethereum;

namespace WalletConnectSharp.Core.Models.BNB
{
    public sealed class BnbApprove : JsonRpcRequest
    {
        [JsonProperty("params")]
        private TransactionData[] _parameters;

        [JsonIgnore]
        public TransactionData[] Parameters => _parameters;

        public BnbApprove(params TransactionData[] transactionDatas) : base()
        {
            this.Method = "approve";
            this._parameters = transactionDatas;
        }
    }
}