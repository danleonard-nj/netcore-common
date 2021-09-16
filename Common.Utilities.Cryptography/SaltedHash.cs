/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Utilities.Cryptography
{
		public class SaltedHash
		{
				public byte[] Salt { get; set; }
				public string Hash { get; set; }

				public SaltedHash(string hash, byte[] salt)
				{
						Salt = salt;
						Hash = hash;
				}
		}
}
