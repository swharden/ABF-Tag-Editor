using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABFtagEditor
{
    class AbfTagEdit
    {

        // version info (update this manually)
        private byte versionMajor = 0;
        private byte versionMinor = 0;
        private byte versionBugFix = 2;
        public string versionString {
            get {
                return $"{versionMajor}.{versionMinor}.{versionBugFix}";
            }
         }

        // ABF properties
        public string abfPath;
            
        public AbfTagEdit(string abfPath = null)
        {
            this.abfPath = abfPath;

            if (abfPath == null)
            {
                Log("ERROR: null abfPath");
                return;
            }
        }

        private string logText;
        private void Log(string msg)
        {
            Console.WriteLine(msg);
            logText += msg + "\n";
        }

        public string GetLog(bool clear = false)
        {
            string textToReturn = logText.Trim();
            if (clear)
                logText = "";
            return textToReturn;
        }
    }
}
