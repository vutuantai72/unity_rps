using Newtonsoft.Json;

namespace WalletConnectSharp.Core.Models.BNB
{
    public sealed class BnbSign : JsonRpcRequest
    {
        [JsonProperty("params")]
        private string[] _parameters;

        [JsonIgnore]
        public string[] Parameters => _parameters;

        public BnbSign(string address, string hexData, string password = "") : base()
        {
            this.Method = "bnb_sign";
            this._parameters = new[] { hexData, address, password };
        }
    }
}