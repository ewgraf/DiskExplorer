using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskExplorer
{
    class Common
    {
        public static List<string> GetTree(string folderPath)
        {
            List<string> directories = Directory.GetDirectories(folderPath).ToList();
            List<string> files = new List<string>();
            foreach (var dir in directories)
            {
                files.AddRange(GetTree(dir));
            }
            files.AddRange(Directory.GetFiles(folderPath, "*.*").ToList());
            return files;
        }

        public static string ByteArrayToHexString(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static long GetDirectorySize(string f)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(f, "*.*");
            string[] d = Directory.GetDirectories(f);
            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a) {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            foreach (string dir in d) {
                b += GetDirectorySize(dir);
            }
            // 4.
            // Return total size
            return b;
        }

        public static async Task<Dictionary<string, List<string>>> Hash(string[] tree)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

            int index = 0;
            double count = 0;
            double delta = tree.Length;

            foreach (var filePath in tree)
            {
                string hash = string.Empty;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        hash = ByteArrayToHexString(md5.ComputeHash(stream));
                    }
                }
                if (!dictionary.ContainsKey(hash))
                {
                    dictionary.Add(hash, new List<string>(new string[] { tree[index] }));
                }
                else
                {
                    dictionary[hash].Add(tree[index]);
                }

                index++;
                //count++;
                //if (count >= delta)
                //{
                //    count = 0;
                    //backgroundWorker1.ReportProgress(1);
                //}
            }
            //backgroundWorker1.ReportProgress(0);
            return dictionary;
        }

        public static void OpenFolderAndSelectFile(string filePath) {
            var arguments = string.Format("/select,\"{0}\"", filePath);
            Process.Start("explorer.exe", arguments);
        }
    }
}
