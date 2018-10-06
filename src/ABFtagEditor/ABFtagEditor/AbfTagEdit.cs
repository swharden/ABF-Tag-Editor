using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private byte versionMajor = 1;
        private byte versionMinor = 0;
        private byte versionBugFix = 0;

        private const int BLOCKSIZE = 512;
        private const int COMMENT_STRING_LENGTH = 56;

        private int tagSectionBlock;
        private int tagSectionBlock_position;
        private int tagSectionItemSize;
        private int tagSectionItemSize_position;
        private int tagSectionItemCount;
        private int tagSectionItemCount_position;

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
                return;

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

        private double abfTagTimeMult;
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

                // the position of tags is to be found at this byte location
                tagSectionBlock_position = 44;
                tagSectionBlock = BytesToInt(FileReadBytes(4, tagSectionBlock_position));

                // size of each tag item is fixed
                tagSectionItemSize_position = -1;
                tagSectionItemSize = 64;

                // the number of tags in the file is defined here
                tagSectionItemCount_position = 48;
                tagSectionItemCount = BytesToInt(FileReadBytes(4, tagSectionItemCount_position));

                // display info about the tag section
                Log($"Tag section: block={tagSectionBlock}, itemSize={tagSectionItemSize}, itemCount={tagSectionItemCount}");
                Log($"Tag section byte positions: ({tagSectionBlock_position}, {tagSectionItemSize_position}, {tagSectionItemCount_position})");

                double fADCSampleInterval = BytesToFloat(FileReadBytes(4, 122));
                abfTagTimeMult = fADCSampleInterval / 1e6;
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

                // compensate for gap-free files
                if (sweepCount == 0)
                    sweepCount = 1;

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
                for (int tagIndex = 0; tagIndex < tagSectionItemCount; tagIndex++)
                {
                    int tagBytePos = tagSectionBlock * BLOCKSIZE + tagIndex * tagSectionItemSize;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(COMMENT_STRING_LENGTH)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    if (nTagType != 1)
                        MessageBox.Show($"Tag {tagIndex + 1} is not a comment tag. It could be damaged by this program.", "WARNING!!!");
                    AbfTag tag = new AbfTag(lTagTime, sComment, abfTagTimeMult, abfSweepLengthSec);
                    tags.Add(tag);
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tag.tagTimeSec} sec), comment: \"{sComment}\"");
                }
            }
            else if (abfVersionMajor == 2)
            {
                // READING TAGS IN ABF2 FILES:

                // the file has certain locations which store byte positions of tag info
                tagSectionBlock_position = 252;
                tagSectionItemSize_position = tagSectionBlock_position + 4;
                tagSectionItemCount_position = tagSectionBlock_position + 8;

                // Read the tagSection to get the () 
                // tagSection information is at byte 252. It contains (4-byte ints): position, itemSize, and itemCount
                fs.Seek(tagSectionBlock_position, System.IO.SeekOrigin.Begin);
                tagSectionBlock = BytesToInt(FileReadBytes(4));
                tagSectionItemSize = BytesToInt(FileReadBytes(4));
                tagSectionItemCount = BytesToInt(FileReadBytes(4));

                // display info about the tag section
                Log($"Tag section: block={tagSectionBlock}, itemSize={tagSectionItemSize}, itemCount={tagSectionItemCount}");
                Log($"Tag section byte positions: ({tagSectionBlock_position}, {tagSectionItemSize_position}, {tagSectionItemCount_position})");

                // read protocolSection (which contains fSynchTimeUnit) needed to convert tagTime to seconds
                // protocolSection is a 4-byte float 14 bytes after the start of the protocolSection
                int protocolSectionFirstBlock = BytesToInt(FileReadBytes(4, 76));
                double fSynchTimeUnit = BytesToFloat(FileReadBytes(4, protocolSectionFirstBlock * BLOCKSIZE + 14));
                abfTagTimeMult = fSynchTimeUnit / 1e6;

                // to determine sweep length, we actually need to know a few things:
                // the data point count is the third value from the section map of the DataSection
                // this is a 4-bit uint32 at byte position 236+4+4
                int dataPointCount = BytesToInt(FileReadBytes(4, 236 + 4 + 4));
                Log($"dataPointCount: {dataPointCount}");

                // sweep count is lActualEpisodes from the header (a uInt32 at byte 12)
                int sweepCount = BytesToInt(FileReadBytes(4, 12));
                abfSweepCount = sweepCount;

                // compensate for gap-free files
                if (sweepCount == 0)
                    sweepCount = 1;

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
                for (int tagIndex = 0; tagIndex < tagSectionItemCount; tagIndex++)
                {
                    int tagBytePos = tagSectionBlock * BLOCKSIZE + tagIndex * tagSectionItemSize;
                    fs.Seek(tagBytePos, System.IO.SeekOrigin.Begin);
                    int lTagTime = BytesToInt(FileReadBytes(4));
                    string sComment = BytesToString(FileReadBytes(COMMENT_STRING_LENGTH)).Trim();
                    int nTagType = BytesToInt(FileReadBytes(2));
                    if (nTagType != 1)
                        MessageBox.Show($"Tag {tagIndex + 1} is not a comment tag. It could be damaged by this program.", "WARNING!!!");
                    AbfTag tag = new AbfTag(lTagTime, sComment, abfTagTimeMult, abfSweepLengthSec);
                    tags.Add(tag);
                    Log($"Tag #{tagIndex + 1}: type {nTagType}, time {lTagTime} ({tag.tagTimeSec} sec), comment: \"{sComment}\"");
                }
            }
        }

        /// <summary>
        /// Write the present tag list into the file
        /// </summary>
        public void WriteTags()
        {
            // open file (ENTIRELY LOCAL SCOPE)
            Log($"Opening ABF file for modification...");
            System.IO.FileStream abfFile = System.IO.File.Open(abfPath, System.IO.FileMode.Open);

            // write the block position
            abfFile.Seek(tagSectionBlock_position, System.IO.SeekOrigin.Begin);
            byte[] bytesBlock = BitConverter.GetBytes(tagSectionBlock);
            Log($"Writing tag section block ({tagSectionBlock}) as {bytesBlock.Length} bytes to position: {abfFile.Position}");
            abfFile.Write(bytesBlock, 0, bytesBlock.Length);

            // write the item size
            if (tagSectionItemSize_position > 0)
            {
                abfFile.Seek(tagSectionItemSize_position, System.IO.SeekOrigin.Begin);
                if (tagSectionItemSize == 0)
                    tagSectionItemSize = 64;
                byte[] bytesItemSize = BitConverter.GetBytes(tagSectionItemSize);
                Log($"Writing tag item size ({tagSectionItemSize}) as {bytesItemSize.Length} bytes to position: {abfFile.Position}");
                abfFile.Write(bytesItemSize, 0, bytesItemSize.Length);
            }

            // write the tag count
            abfFile.Seek(tagSectionItemCount_position, System.IO.SeekOrigin.Begin);
            byte[] bytesTagCount = BitConverter.GetBytes(tags.Count);
            Log($"Writing tag count ({tags.Count}) as {bytesTagCount.Length} bytes to position: {abfFile.Position}");
            abfFile.Write(bytesTagCount, 0, bytesTagCount.Length);

            for (int i = 0; i < tags.Count; i++)
            {
                AbfTag tag = tags[i];
                Log($"Writing content of tag {i + 1}");

                // seek to this tag location
                int byteLocation = tagSectionBlock * BLOCKSIZE + i * tagSectionItemSize;
                abfFile.Seek(byteLocation, System.IO.SeekOrigin.Begin);

                // write the tag location (4 bytes)
                byte[] bytesTagTime = BitConverter.GetBytes(tag.tagTime);
                Log($"Writing tag time ({tag.tagTime}) as {bytesTagTime.Length} bytes to position: {abfFile.Position}");
                abfFile.Write(bytesTagTime, 0, bytesTagTime.Length);

                // write the tag comment (56 bytes)
                string sComment = tag.comment.PadRight(COMMENT_STRING_LENGTH, ' ');
                byte[] bytesComment = Encoding.ASCII.GetBytes(sComment);
                Log($"Writing comment ({tag.comment}) as {bytesComment.Length} bytes to position: {abfFile.Position}");
                abfFile.Write(bytesComment, 0, bytesComment.Length);

                // write the tag type (2 bytes)
                int tagType = 1;
                byte[] bytesTagType = BitConverter.GetBytes(tagType);
                Log($"Writing tag type ({tagType}) as {bytesTagType.Length} bytes to position: {abfFile.Position}");
                abfFile.Write(bytesTagType, 0, bytesTagType.Length);

            }

            abfFile.Close();
            Log($"ABF file closed.");
            Log("ABF file writing complete!");
        }

        private long AddBlock(int blocksToAdd = 1)
        {
            // add an extra block of bytes to the end of the file
            byte[] bytes = new byte[BLOCKSIZE * blocksToAdd];
            var stream = new System.IO.FileStream(abfPath, System.IO.FileMode.Append);
            stream.Seek(0, System.IO.SeekOrigin.End);
            long byteLocationOfNewData = stream.Position;
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            Log($"Wrote {bytes.Length} bytes at position: {byteLocationOfNewData}");
            return byteLocationOfNewData;
        }

        public void AddTag(string comment = "new tag comment", double tagTimeSec = 0)
        {

            if (tagSectionItemCount==0)
            {
                // no comments exist in this file, so make a new tagSection from scratch and update the map
                Log("Creating new tag section from scratch");
                long fileSizeBytes = new System.IO.FileInfo(abfPath).Length;
                long bytePositionNewBlock = AddBlock(2);
                tagSectionBlock = (int)(bytePositionNewBlock / BLOCKSIZE);
                tagSectionItemCount += 1;
            }

            AbfTag tag = new AbfTag(0, "", abfTagTimeMult, abfSweepLengthSec);
            tag.SetComment(comment);
            tag.SetTimeSec(tagTimeSec);
            tags.Add(tag);

            Log($"Created Tag #{tags.Count}");
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
