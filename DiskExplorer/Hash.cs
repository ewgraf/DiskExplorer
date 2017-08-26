using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DiskExplorer
{
	public class Hash
	{
		// https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
		public static string GetSHA256(byte[] bytes)
		{
			SHA256Managed hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(bytes);
            return BytesToHex(hash);
        }

        public static string GetFileHash(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(fileStream);
                return BytesToHex(hash);
            }
        }

        public static string BytesToHex(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (byte x in hash) {
                sb.AppendFormat("{0:x2}", x);
            }
            return sb.ToString();
        }

        public static string SuperFastHashUnsafeFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                ulong result = 0;
                int bytesRead;
                var buffer = new byte[134217728]; // 134217728 100 Mb  1073741824 1 Gb
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                    var tmpBuffer = new byte[bytesRead];
                    Array.Copy(buffer, 0, tmpBuffer, 0, bytesRead);
                    result += SuperFastHashUnsafe(tmpBuffer);
                    // TODO: Process bytesRead number of bytes from the buffer
                    // not the entire buffer as the size of the buffer is 1KB
                    // whereas the actual number of bytes that are read are 
                    // stored in the bytesRead integer.
                }
                return result.ToString();
            }
        }


        public static unsafe ulong SuperFastHashUnsafe(byte[] dataToHash)
        {
            long dataLength = dataToHash.Length;
            if (dataLength == 0) {
                return 0;
            }                
            ulong hash = (ulong)dataLength;
            long remainingBytes = dataLength & 3; // mod 4
            long numberOfLoops = dataLength >> 2; // div 4

            fixed (byte* firstByte = &(dataToHash[0])) {
                /* Main loop */
                UInt16* data = (UInt16*)firstByte;
                for (; numberOfLoops > 0; numberOfLoops--) {
                    hash += *data;
                    ulong tmp = (ulong)(*(data + 1) << 11) ^ hash;
                    hash = (hash << 16) ^ tmp;
                    data += 2;
                    hash += hash >> 11;
                }
                switch (remainingBytes) {
                    case 3:
                        hash += *data;
                        hash ^= hash << 16;
                        hash ^= ((ulong)(*(((byte*)(data)) + 2))) << 18;
                        hash += hash >> 11;
                        break;
                    case 2:
                        hash += *data;
                        hash ^= hash << 11;
                        hash += hash >> 17;
                        break;
                    case 1:
                        hash += *((byte*)data);
                        hash ^= hash << 10;
                        hash += hash >> 1;
                        break;
                    default:
                        break;
                }
            }

            /* Force "avalanching" of final 127 bits */
            hash ^= hash << 3;
            hash += hash >> 5;
            hash ^= hash << 4;
            hash += hash >> 17;
            hash ^= hash << 25;
            hash += hash >> 6;

            return hash;
        }
    }
}
