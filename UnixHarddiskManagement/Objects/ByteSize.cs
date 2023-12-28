using System;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.Objects
{
    public class ByteSize
    {
        /// <summary>
        /// the precise byte size. Can be get or set.
        /// </summary>
        public ulong Bytes { get; set; }

        /// <summary>
        /// creates a Sizeinfo with initial value of 0
        /// </summary>
        public ByteSize()
        {
            this.Bytes = 0;
        }

        /// <summary>
        /// creates a Sizeinfo from a floating point unit
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        public ByteSize(SizeUnit unit, double value)
        {
            SetSize(unit, value);
        }

        /// <summary>
        /// creates a Sizeinfo from a precise floating point unit
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        public ByteSize(SizeUnit unit, decimal value)
        {
            SetSize(unit, value);
        }

        /// <summary>
        /// creates a Sizeinfo from a precise size
        /// </summary>
        /// <param name="bytes"></param>
        public ByteSize(ulong bytes)
        {
            Bytes = bytes;
        }

        /// <summary>
        /// returns the floating point size in the requested unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public double GetSize(SizeUnit unit)
        {
            return Bytes / (double)unit;
        }

        /// <summary>
        /// returns the precise floating point size in the requested unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public decimal GetSizeDecimal(SizeUnit unit)
        {
            return Bytes / (decimal)unit;
        }

        /// <summary>
        /// sets the value according to the unit and floating point size
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetSize(SizeUnit unit, double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

            Bytes = (ulong)(value * (double)unit);
        }

        /// <summary>
        /// sets a precise value according to a unit and a floating point decimal value
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetSize(SizeUnit unit, decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

            Bytes = (ulong)(value * (decimal)unit);
        }
    }
}
