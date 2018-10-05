using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABFtagEditor
{
    class AbfTag
    {
        public int tagTime { get; private set; }
        public string comment { get; set; }
        public double tagTimeMult { get; }
        public double sweepLengthSec { get; }
        public double tagTimeSec { get { return tagTime * tagTimeMult; } set { tagTime = (int)(value / tagTimeMult); } }
        public double tagTimeMin { get { return tagTimeSec / 60.0; } }
        public double tagTimeSweep { get { return tagTimeSec / sweepLengthSec; } }
        public string description { get { return string.Format("{0} @ {1:0.00} min (sweep {2:0})", comment, tagTimeMin, tagTimeSweep); } }

        public AbfTag(int tagTime, string comment, double tagTimeMult, double sweepLengthSec)
        {
            this.tagTime = tagTime;
            this.comment = comment;
            this.tagTimeMult = tagTimeMult;
            this.sweepLengthSec = sweepLengthSec;
        }

        public void SetComment(string comment)
        {
            this.comment = comment;
        }

        public void SetTimeSec(double timeSec)
        {
            tagTime = (int)(timeSec / tagTimeMult);
        }

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
        public List<AbfTag> tags = new List<AbfTag>();

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

        public double abfTotalLengthSec;
        public double abfSweepLengthSec;
        public int abfSweepCount;
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
                double tagTimeMult = fADCSampleInterval / 1e6;
                Log($"fADCSampleInterval: {fADCSampleInterval}");

                // to get the sweep lenght, we need to know a lot of things:
                double dataRate = 1e6 / fADCSampleInterval;
                Log($"dataRate: {dataRate}");

                // lActualAcqLength (4-byte signed int @ byte 10)
                int dataPointCount = BytesToInt(FileReadBytes(4, 10));
                Log($"dataPointCount: {dataPointCount}");

                // lActualEpisodes (4-byte signed int @ byte 16)
                int sweepCount = BytesToInt(FileReadBytes(4, 16));
                abfSweepCount = sweepCount;
                Log($"sweepCount: {sweepCount}");

                // nADCNumChannels (2-byte signed int @ byte 120)
                int channelCount = BytesToInt(FileReadBytes(2, 120));
                Log($"channelCount: {channelCount}");

                // now you can claculate sweep length in seconds
                abfTotalLengthSec = dataPointCount / channelCount / dataRate;
                Log($"abfTotalLengthSec: {abfTotalLengthSec}");
                abfSweepLengthSec = dataPointCount / sweepCount / channelCount / dataRate;
                Log($"sweepLengthSec: {abfSweepLengthSec}");

                // loop across the tags and add them to the list
                int tagLengthBytes = 64;
                for (int tagIndex = 0; tagIndex < lNumTagEntries; tagIndex++)
                {
                    int tagBytePos = lTagSectionPtr * BLOCKSIZE + tagIndex * tagLengthBytes;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(56)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    double tagTimeSec = lTagTime * tagTimeMult;
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tagTimeSec} sec), comment: \"{sComment}\"");
                    tags.Add(new AbfTag(lTagTime, sComment, tagTimeMult, abfSweepLengthSec));
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

                // read protocolSection (which contains fSynchTimeUnit) needed to convert tagTime to seconds
                // protocolSection is a 4-byte float 14 bytes after the start of the protocolSection
                int protocolSectionFirstBlock = BytesToInt(FileReadBytes(4, 76));
                double fSynchTimeUnit = BytesToFloat(FileReadBytes(4, protocolSectionFirstBlock * BLOCKSIZE + 14));
                double tagTimeMult = fSynchTimeUnit / 1e6;

                // to determine sweep length, we actually need to know a few things:
                // the data point count is the third value from the section map of the DataSection
                // this is a 4-bit uint32 at byte position 236+4+4
                int dataPointCount = BytesToInt(FileReadBytes(4, 236 + 4 + 4));
                Log($"dataPointCount: {dataPointCount}");

                // sweep count is lActualEpisodes from the header (a uInt32 at byte 12)
                int sweepCount = BytesToInt(FileReadBytes(4, 12));
                abfSweepCount = sweepCount;
                Log($"sweepCount: {sweepCount}");

                // channel count comes from the number of sections in the ADCSection map (byte 92+4+4)
                int channelCount = BytesToInt(FileReadBytes(4, 92 + 4 + 4));
                Log($"channelCount: {channelCount}");

                // fADCSequenceInterval is a 4-byte float two bytes into the protocol section
                int protocolSectionFirstByte = BytesToInt(FileReadBytes(4, 76)) * BLOCKSIZE;
                double fADCSequenceInterval = BytesToFloat(FileReadBytes(4, protocolSectionFirstByte + 2));
                Log($"fADCSequenceInterval: {fADCSequenceInterval}");

                // sample rate is the inverse of fADCSequenceInterval (in microseconds)
                double dataRate = 1e6 / fADCSequenceInterval;
                Log($"dataRate: {dataRate}");

                // now you can claculate sweep length in seconds
                abfTotalLengthSec = dataPointCount / channelCount / dataRate;
                Log($"abfTotalLengthSec: {abfTotalLengthSec}");
                abfSweepLengthSec = dataPointCount / sweepCount / channelCount / dataRate;
                Log($"sweepLengthSec: {abfSweepLengthSec}");

                // loop across the tags and add them to the list
                for (int tagIndex = 0; tagIndex < tagCount; tagIndex++)
                {
                    int tagBytePos = tagSectionFirstBlock * BLOCKSIZE + tagIndex * tagSizeBytes;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(56)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    double tagTimeSec = lTagTime * tagTimeMult;
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tagTimeSec} sec), comment: \"{sComment}\"");
                    tags.Add(new AbfTag(lTagTime, sComment, tagTimeMult, abfSweepLengthSec));
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
