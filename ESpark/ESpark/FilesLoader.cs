using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace ESpark
{
    class FilesLoader
    {
        public string set_text(string text, string path)
        {
            StreamWriter w1 = new StreamWriter(path);
            w1.Write(text);
            w1.Close();
            return "";
        }
        public string get_text(string path)
        {
            string answer = "";
            var lines = File.ReadAllText(path);
            foreach (var line in lines)
            {
                answer = answer + "\n" + line;
            }
            return RemoveSpecialChars(answer);
        }
        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._ ]", "");
        }
    }
}
