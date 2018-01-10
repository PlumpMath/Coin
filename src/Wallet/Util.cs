using System;
using System.Text;

namespace Coin.Wallet
{
    public static class Util
    {
        public static string ByteArrayToHex(byte[] byteArray)
        {
            StringBuilder Sb = new StringBuilder();

            foreach (Byte b in byteArray)
                Sb.Append(b.ToString("x2"));

            return Sb.ToString();
        }
    }
}