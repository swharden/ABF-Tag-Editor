using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABFtagEditor
{
    class tag
    {
        public double timeSec;
        public double timeMin;
        public double timeSweep;
        public double time;
        public string comment;
    }

    class AbfTagEdit
    {

        // version info (update this manually)
        private byte versionMajor = 0;
        private byte versionMinor = 0;
        private byte versionBugFix = 2;
        public string versionString
        {
            get
            {
                return $"{versionMajor}.{versionMinor}.{versionBugFix}";
            }
        }

        // Tags
        public List<tag> tags = new List<tag>();

        // ABF properties
        public string abfPath;
        public byte abfVersionMajor;

        // Class objects
        private System.IO.FileStream fs;
        private System.IO.BinaryReader br;

        /// <summary>
        /// Class to edit comment tags of ABF1 and ABF2 files.
        /// </summary>
        public AbfTagEdit(string abfPath = null)
        {
            this.abfPath = abfPath;

            if (abfPath == null)
                throw new Exception("null abfPath");

            else if (!System.IO.File.Exists(abfPath))
                throw new Exception($"file does not exist: {abfPath}");
            
            FileOpen();
            ReadFileFormat();
            FileClose();
        }


        ////////////////////////////////////////////////////////////////////////
        // ABF HEADER READING

        private void ReadFileFormat()
        {
            // determine the ABF file format
            string firstFour = BytesToString(FileReadBytes(4, 0));
            if (firstFour == "ABF ")
                abfVersionMajor = 1;
            else if (firstFour == "ABF2")
                abfVersionMajor = 2;
            else
                throw new Exception("File is not a valid ABF1 or ABF2 file.");
            Log($"File is in the ABF{abfVersionMajor} format.");
        }

        private byte[] FileReadBytes(int count, int position = -1)
        {
            if (position >= 0)
                br.BaseStream.Seek(position, System.IO.SeekOrigin.Begin);
            return br.ReadBytes(count); ;
        }


        ////////////////////////////////////////////////////////////////////////
        // BINARY DATA / VARIABLE CONVERSION

        private string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        ////////////////////////////////////////////////////////////////////////
        // BINARY FILE ACCESS

        private void FileOpen(bool readOnly = true)
        {
            fs = System.IO.File.Open(abfPath, System.IO.FileMode.Open);
            br = new System.IO.BinaryReader(fs);
        }

        private void FileClose()
        {
            br.Close();
            fs.Close();
        }

        ////////////////////////////////////////////////////////////////////////
        //// DEVELOPER TOOLS

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
