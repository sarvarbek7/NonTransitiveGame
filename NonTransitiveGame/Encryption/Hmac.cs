using System.Security.Cryptography;
using System.Text;

namespace NonTransitiveGame.Encryption
{
	public static class Hmac
	{
		public static byte[] HashData(string move, byte[] key)
		{
			byte[] source = Encoding.UTF8.GetBytes(move);

			return HMACSHA256.HashData(key, source);
		}
	}
}
