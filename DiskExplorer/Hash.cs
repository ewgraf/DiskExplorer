using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DiskExplorer
{
	public class Hash
	{
        public static long MaxBufLengthPerCore = 134217728;//2147483648 / Environment.ProcessorCount;
        // 134217728 100 Mb  536870912 512 Mb  1073741824 1 Gb  2147483648 2 Gb - max VirtualMemory at .net 4.7  27.08.2017 18:04 GMT+3
        // https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        public static string GetSHA256(byte[] bytes) {
			SHA256Managed hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(bytes);
            return BytesToHex(hash);
        }

        public static string GetSHA1(string filePath) {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                SHA1Managed hashstring = new SHA1Managed();
                byte[] hash = hashstring.ComputeHash(fileStream);
                return BytesToHex(hash);
            }
        }

        public static string GetFileHash(string filePath) {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(fileStream);
                return BytesToHex(hash);
            }
        }

        public static string BytesToHex(byte[] hash) {
            var sb = new StringBuilder();
            foreach (byte x in hash) {
                sb.AppendFormat("{0:x2}", x);
            }
            return sb.ToString();
        }

        public static string SuperFastHashUnsafeFile(string filePath) {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                ulong result = 0;
                int bytesRead;
                var buffer = new byte[MaxBufLengthPerCore];
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                    result += SuperFastHashUnsafe(buffer, bytesRead);
                    // TODO: Process bytesRead number of bytes from the buffer
                    // not the entire buffer as the size of the buffer is 1KB
                    // whereas the actual number of bytes that are read are 
                    // stored in the bytesRead integer.
                }
                return result.ToString();
            }
        }

        // http://landman-code.blogspot.ru/2009/02/c-superfasthash-and-murmurhash2.html
        public static unsafe ulong SuperFastHashUnsafe(byte[] dataToHash, long dataLength) {
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
