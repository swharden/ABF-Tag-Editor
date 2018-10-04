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

        private const int BLOCKSIZE = 512;

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
            ReadTags();
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
            Log($"This file is of the ABF{abfVersionMajor} format.");
        }

        private void ReadTags()
        {
            if (abfVersionMajor == 1)
            {
                // READING TAGS IN ABF1 FILES:
                // the tag position in memory is lTagSectionPtr (signed int at byte 44)
                // the number of tags is lNumTagEntries (signed int at byte 48)
                // each tag structure is (int32, "56s", short) (lTagTime, sComment, nTagType)
                // this makes each tag 64 bytes long.
                // fADCSampleInterval is needed to convert lTagTime to seconds

                int lTagSectionPtr = BytesToInt(FileReadBytes(4, 44));
                int lNumTagEntries = BytesToInt(FileReadBytes(4, 48));
                double fADCSampleInterval = BytesToFloat(FileReadBytes(4, 122));

                Log($"{lNumTagEntries} tags found in ABF file");
                int tagLengthBytes = 64;
                for (int tagIndex = 0; tagIndex < lNumTagEntries; tagIndex++)
                {
                    int tagBytePos = lTagSectionPtr * BLOCKSIZE + tagIndex * tagLengthBytes;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(56)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    double tagTimeSec = lTagTime * fADCSampleInterval / 1e6;
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tagTimeSec} sec), comment: \"{sComment}\"");
                }
            }
            else if (abfVersionMajor == 2)
            {
                // READING TAGS IN ABF2 FILES:
                // Read the tagSection to get the () 
                // tagSection information is at byte 252. It contains (4-byte ints): position, itemSize, and itemCount
                fs.Seek(252, System.IO.SeekOrigin.Begin);
                int tagSectionFirstBlock = BytesToInt(FileReadBytes(4));
                int tagSizeBytes = BytesToInt(FileReadBytes(4));
                int tagCount = BytesToInt(FileReadBytes(4));
                Log($"{tagCount} tags found in ABF file");

                // read protocolSection (which contains fSynchTimeUnit) needed to convert tagTime to seconds
                // protocolSection is a 4-byte float 14 bytes after the start of the protocolSection
                int protocolSectionFirstBlock = BytesToInt(FileReadBytes(4, 76));
                double fSynchTimeUnit = BytesToFloat(FileReadBytes(4, protocolSectionFirstBlock * BLOCKSIZE + 14));
                
                for (int tagIndex = 0; tagIndex < tagCount; tagIndex++)
                {
                    int tagBytePos = tagSectionFirstBlock * BLOCKSIZE + tagIndex * tagSizeBytes;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(56)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    double tagTimeSec = lTagTime * fSynchTimeUnit / 1e6;
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tagTimeSec} sec), comment: \"{sComment}\"");
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // BINARY FILE ACCESS AND CONVERSION

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

        private byte[] FileReadBytes(int count, int position = -1)
        {
            if (position >= 0)
                br.BaseStream.Seek(position, System.IO.SeekOrigin.Begin);
            return br.ReadBytes(count); ;
        }

        private int BytesToInt(byte[] bytes)
        {
            if (bytes.Length == 2)
                return BitConverter.ToInt16(bytes, 0);
            else if (bytes.Length == 4)
                return BitConverter.ToInt32(bytes, 0);
            else
                throw new Exception("float must have 4 (single) or 8 (double) bytes");
        }

        private string BytesToString(byte[] bytes)
        {
            string text;
            //text = BitConverter.ToString(bytes); // a string of numbers
            text = Encoding.Default.GetString(bytes); // evaluate bytes as ASCII
            return text;
        }

        private double BytesToFloat(byte[] bytes)
        {
            if (bytes.Length == 4)
                return (double)BitConverter.ToSingle(bytes, 0);
            else if (bytes.Length == 8)
                return (double)BitConverter.ToSingle(bytes, 0);
            else
                throw new Exception("float must have 4 (single) or 8 (double) bytes");
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
