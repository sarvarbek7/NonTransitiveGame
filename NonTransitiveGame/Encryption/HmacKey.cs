using System.Security.Cryptography;

namespace NonTransitiveGame.Encryption
{
	public class HmacKey
	{
		public static byte[] GenerateKey()
		{
			return RandomNumberGenerator.GetBytes(32);
		}
	}
}