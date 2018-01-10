using System;
using Newtonsoft.Json;

namespace Coin.Wallet
{
    public class Keystore
    {
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }
        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("version")]
        public Version Version { get; set; }
    }
}