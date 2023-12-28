namespace UnixHarddiskManagement.Enumerators
{
    public enum SizeUnit : ulong
    {
        /// <summary>
        /// byte
        /// </summary>
        b = 1UL,
        /// <summary>
        /// kilo byte
        /// </summary>
        kb = 1024UL,
        /// <summary>
        /// mega byte
        /// </summary>
        mb = 1024UL * 1024,
        /// <summary>
        /// giga byte
        /// </summary>
        gb = 1024UL * 1024 * 1024,
        /// <summary>
        /// terra byte
        /// </summary>
        tb = 1024UL * 1024 * 1024 * 1024,
        /// <summary>
        /// peta byte
        /// </summary>
        pb = 1024UL * 1024 * 1024 * 1024 * 1024,
        /// <summary>
        /// exa byte
        /// </summary>
        eb = 1024UL * 1024 * 1024 * 1024 * 1024 * 1024,
        
    }
}
