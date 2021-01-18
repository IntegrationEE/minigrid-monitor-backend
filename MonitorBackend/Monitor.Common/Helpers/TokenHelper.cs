using System;
using System.Linq;
using System.Text;

namespace Monitor.Common.Helpers
{
    public static class TokenHelper
    {
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <returns></returns>
        public static string Generate()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            var tokenData = time.Concat(key).ToArray();

            StringBuilder hex = new StringBuilder(tokenData.Length * 2);
            foreach (byte b in tokenData)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
        /// <summary>
        /// Verify Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsValid(string token)
        {
            int chars = token.Length;
            byte[] data = new byte[chars / 2];
            for (int i = 0; i < chars; i += 2)
                data[i / 2] = Convert.ToByte(token.Substring(i, 2), 16);

            DateTime generatedDate = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            return generatedDate >= DateTime.UtcNow.AddHours(-24);
        }
    }
}
