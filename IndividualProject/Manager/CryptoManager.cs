using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject.Manager
{
	public static class CryptoManager
	{
		public static string EncryptPassword(string plainText, out string Base64salt)
		{
			var salt = GetSalt(32);

			var hash = Hash(plainText, salt);

			Base64salt = Convert.ToBase64String(salt);

			var Base64hash = Convert.ToBase64String(hash);

			return Base64hash;
		}

		public static bool ComparePassword(string plainText, string DBHash, string DBSalt)
		{
			var salt = Convert.FromBase64String(DBSalt);

			var hash = Hash(plainText, salt);

			var encryptedHash = Convert.ToBase64String(hash);

			return encryptedHash == DBHash;
		}

		private static byte[] GetSalt(int maximumSaltLength)
		{
			var salt = new byte[maximumSaltLength];
			using (var random = new RNGCryptoServiceProvider())
			{
				random.GetNonZeroBytes(salt);
			}

			return salt;
		}

		private static byte[] Hash(string value, byte[] salt)
		{
			return Hash(Encoding.UTF8.GetBytes(value), salt);
		}

		private static byte[] Hash(byte[] value, byte[] salt)
		{
			byte[] saltedValue = value.Concat(salt).ToArray();

			return new SHA256Managed().ComputeHash(saltedValue);
		}
	}
}
