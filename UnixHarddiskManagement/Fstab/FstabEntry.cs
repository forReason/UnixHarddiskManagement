using System.Text.RegularExpressions;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.Fstab
{
    public class FstabEntry
    {
        /// <summary>
        /// to read a line or to create a comment (#comment)
        /// </summary>
        /// <param name="line"></param>
        public FstabEntry(string line)
        {

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                this.Comment = line;
                return;
            }

            string[] components = Regex.Split(line.Trim(), @"\s+");
            if (components.Length < 6)
            {
                throw new FormatException("Line does not have enough components to parse an fstab entry.");
            }

            this.Identifier = components[0];
            this.MountingPoint = components[1];
            this.SystemType = JsonConverters.Converter.ParseFileSystemType(components[2]);
            this.MountingOptions = FstabMountingOption.GetOptions(components[3]);

            if (!int.TryParse(components[4], out int dump))
            {
                throw new FormatException("Invalid format for dump value.");
            }
            this.Dump = dump;

            if (!int.TryParse(components[5], out int pass))
            {
                throw new FormatException("Invalid format for pass value.");
            }
            this.Pass = (FstabPassOption)pass;
        }

        // Second constructor for direct instantiation
        public FstabEntry(string identifier, string mountPoint, FileSystemType systemType, FstabMountingOption[] options, FstabPassOption pass)
        {
            this.Identifier = identifier;
            this.MountingPoint = mountPoint;
            this.SystemType = systemType;
            this.MountingOptions = options;
            this.Pass = pass;
            this.Dump = 0;
        }

        public string Identifier { get; set; }
        public string MountingPoint { get; set; }
        public FileSystemType SystemType { get; set; }
        public FstabMountingOption[] MountingOptions { get; set; }
        public int Dump { get; set; }
        public FstabPassOption Pass { get; set; }
        public string Comment { get; set; } // this line is a comment, omit
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.Comment))
            {
                return this.Comment;
            }
            else
            {
                return $"{this.Identifier}\t{this.MountingPoint}\t{this.SystemType}\t{FstabMountingOption.GetOptionString(this.MountingOptions)}\t{this.Dump}\t{(int)this.Pass}";
            }
        }
    }
}
