using System.Runtime.CompilerServices;
using UnixHarddiskManagement.Enumerators;
using UnixHarddiskManagement.JsonConverters;

namespace UnixHarddiskManagement.Tests
{
    public class FileSystemTypeParserTests
    {
        [Theory]
        [InlineData("ext4", FileSystemType.EXT4)]
        [InlineData("NTFS", FileSystemType.NTFS)]
        [InlineData("FAT32", FileSystemType.FAT32)]
        [InlineData("LVM2_member", FileSystemType.LVM2Member)]
        // Add other valid cases here
        public void ParseFileSystemType_ValidInputs(string input, FileSystemType expected)
        {
            var result = Converter.ParseFileSystemType(input); // Replace 'YourClass' with the actual class name
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ParseFileSystemType_NullOrEmpty(string input)
        {
            var result = Converter.ParseFileSystemType(input);
            Assert.Equal(FileSystemType.Other, result); // Assuming 'default' maps to 'Other'
        }

        [Fact]
        public void ParseFileSystemType_InvalidInput()
        {
            string invalidInput = "invalid_fs_type";
            var result = Converter.ParseFileSystemType(invalidInput);
            Assert.Equal(FileSystemType.Other, result); // Assuming 'default' maps to 'Other'
        }

        // Add more test cases as needed...
    }
}
