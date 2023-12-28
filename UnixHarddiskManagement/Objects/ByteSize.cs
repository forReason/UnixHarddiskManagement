using System;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.Objects
{
    /// <summary>
    /// Represents a size in different units.
    /// </summary>
    public class ByteSize
    {
        /// <summary>
        /// Gets or sets the unit of measurement for the size.
        /// </summary>
        public SizeUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the value in the specified unit.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the Size class with specified unit and value.
        /// </summary>
        /// <param name="unit">The unit of measurement.</param>
        /// <param name="value">The value in the specified unit.</param>
        public ByteSize(SizeUnit unit, double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

            Unit = unit;
            Value = value;
        }

        /// <summary>
        /// Gets the size in bytes.
        /// </summary>
        public ulong Bytes => (ulong)((ulong)Unit * Value);

        /// <summary>
        /// Sets the size based on a byte count, approximating to the nearest unit.
        /// </summary>
        /// <param name="bytes">The size in bytes.</param>
        public void SetSizeFromBytes(ulong bytes)
        {
            if (bytes == 0)
            {
                Unit = SizeUnit.b;
                Value = 0;
                return;
            }

            SizeUnit[] units = (SizeUnit[])Enum.GetValues(typeof(SizeUnit));
            SizeUnit selectedUnit = SizeUnit.b;
            double value = bytes;

            for (int i = units.Length - 1; i >= 0; i--)
            {
                double convertedValue = bytes / (double)units[i];
                if (convertedValue >= 1)
                {
                    selectedUnit = units[i];
                    value = convertedValue;
                    break;
                }
            }

            Unit = selectedUnit;
            Value = value;
        }
    }
}
