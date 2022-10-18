using Newtonsoft.Json;

namespace WalletConnectSharp.Core.Models.Ethereum
{
    public class WalletSwitchEthChain : JsonRpcRequest
    {
        [JsonProperty("params")] 
        private SwitchChainData[] _parameters;

        [JsonIgnore]
        public SwitchChainData[] Parameters => _parameters;

        public WalletSwitchEthChain(params SwitchChainData[] chainData) : base()
        {
            this.Method = "wallet_switchEthereumChain";
            this._parameters = chainData;
        }
    }
}