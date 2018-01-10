using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Coin.Wallet
{
    public class Address
    {
        public static Version AddressAlgorithmVersion => new Version(1,0);
        private  readonly RSACryptoServiceProvider rsaService;

        public Address() { }
        public Address(RSAParameters privateKeyParameters)
        {
            rsaService = new RSACryptoServiceProvider();
            rsaService.ImportParameters(privateKeyParameters);
        }
        public static Keystore GenerateAddress(string password)
        {
            //Generate a public/private key pair.  
            using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                //Save the public key information to an RSAParameters structure.  
                RSAParameters publicKeyParameters = RSA.ExportParameters(false);  
                var publicKeyJson = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(publicKeyParameters));
                var publicKeyFingerprint = CalculateFingerprint(publicKeyJson);
                var publicKeyBase64 = Convert.ToBase64String(publicKeyJson);

                RSAParameters privateKeyParameters = RSA.ExportParameters(true);
                var privateKeyJson = JsonConvert.SerializeObject(privateKeyParameters);
                var encodedPrivateKey = StringCipher.Encrypt(privateKeyJson, password);
                var privateKeyBytes = Encoding.UTF8.GetBytes(encodedPrivateKey);
                var privateKeyBase64 = Convert.ToBase64String(privateKeyBytes);

                return new Keystore() {
                    PublicKey = publicKeyBase64,
                    PrivateKey = privateKeyBase64,
                    Fingerprint = publicKeyFingerprint,
                    CreatedAt = DateTime.UtcNow,
                    Version = AddressAlgorithmVersion,
                };
            }
        }

        public void TrustAddress(string address)
        {
            byte[] addressFingerprintByteArray = new byte[address.Length / 2];
            for (int index = 0; index < addressFingerprintByteArray.Length; index++)
            {
                string byteValue = address.Substring(index * 2, 2);
                addressFingerprintByteArray[index] = 
                    byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            rsaService.SignHash(Encoding.UTF8.GetBytes(address), CryptoConfig.MapNameToOID("SHA256"));
        }

        public static Address Unlock(string privateKey, string password)
        {
            var privateKeyJson = StringCipher.Decrypt(privateKey, password);
            var privateKeyParameters = JsonConvert.DeserializeObject<RSAParameters>(privateKey);
            return new Address(privateKeyParameters);
        }

        public static string CalculateFingerprint (byte[] buffer)
        {
            var fingerprint = "";

            using (SHA256 hash = SHA256Managed.Create()) {
                var result = hash.ComputeHash(buffer);

                fingerprint = Util.ByteArrayToHex(result);
            }

            return fingerprint;
        }
    }
}
