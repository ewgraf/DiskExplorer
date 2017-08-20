using System.Security.Cryptography;

namespace DiskExplorer
{
	public class Hash
	{
		// https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
		public static string GetSHA256(byte[] bytes)
		{
			//byte[] bytes = Encoding.Unicode.GetBytes(text);
			SHA256Managed hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(bytes);
			string hashString = string.Empty;
			foreach (byte x in hash) {
				hashString += string.Format("{0:x2}", x);
			}
			return hashString;
		}
	}
}
