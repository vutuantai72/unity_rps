using Newtonsoft.Json;
using WalletConnectSharp.Core.Models.Ethereum;

namespace WalletConnectSharp.Core.Models.BNB
{
    public sealed class BnbSwapTokenToCoin : JsonRpcRequest
    {
        [JsonProperty("params")]
        private TransactionData[] _parameters;

        [JsonIgnore]
        public TransactionData[] Parameters => _parameters;

        public BnbSwapTokenToCoin(params TransactionData[] transactionDatas) : base()
        {
            this.Method = "swapTokenToCoin";
            this._parameters = transactionDatas;
        }
    }
}