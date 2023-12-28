using UnixHarddiskManagement.Objects;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.Tests
{
    public class SizeTests
    {
        [Theory]
        [InlineData(SizeUnit.b, 1000, 1000)]
        [InlineData(SizeUnit.kb, 1, 1024)]
        [InlineData(SizeUnit.mb, 1, 1024 * 1024)]
        [InlineData(SizeUnit.gb, 1, 1024L * 1024 * 1024)]
        // Add more test cases as needed
        public void Size_Bytes_ShouldReturnCorrectByteCount(SizeUnit unit, double value, ulong expectedBytes)
        {
            // Arrange
            var size = new ByteSize(unit, value);

            // Act
            var bytes = size.Bytes;

            // Assert
            Assert.Equal(expectedBytes, bytes);
        }

        [Theory]
        [InlineData(0, SizeUnit.b, 0)]
        [InlineData(1024, SizeUnit.kb, 1)]
        [InlineData(1024 * 1024, SizeUnit.mb, 1)]
        [InlineData(1024L * 1024 * 1024, SizeUnit.gb, 1)]
        // Add more test cases as needed
        public void SetSizeFromBytes_ShouldSetCorrectUnitAndValue(ulong bytes, SizeUnit expectedUnit, double expectedValue)
        {
            // Arrange
            var size = new ByteSize(SizeUnit.b, 0);

            // Act
            size.SetSizeFromBytes(bytes);

            // Assert
            Assert.Equal(expectedUnit, size.Unit);
            Assert.Equal(expectedValue, size.Value, 5); // 5 decimal places precision
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentOutOfRangeExceptionForNegativeValue()
        {
            // Arrange, Act, and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ByteSize(SizeUnit.b, -1));
        }

        // Additional tests can be added to cover more scenarios
    }
}
