using System;

namespace SAE_SUBNETTING
{
    /// Added by: Manuel
    /// <summary>
    /// Needed class for counting set bits in a UInt32.
    /// This was the fastest performing algorythm I was able to fully understand: https://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetTable
    /// Self-implementation is needed since the BitOperations.PopCount function is only availible from dotnet sdk 3.0 and newer.
    /// This time I wanted to use the same environment as on the school computers (dotnet sdk 2.1.700).
    /// </summary>
    public static class Bits
    {
        // lookup table for number of set bits in one byte
        private static readonly int[] lookupTable = new int[256] {
                0,1,                                                                // only using  one bit
                1,2,                                                                // only using  two bits
                1,2,2,3,                                                            // only using tree bits
                1,2,2,3,2,3,3,4,                                                    // only using four bits
                1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,                                    //      using five bits
                1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,    //      using  six bits
                1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,    // using seven bits
                1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8     // using all eight bits
            };

        // defining where each octet is located in the UInt32
        private const UInt32 first_octet = 0xff000000;
        private const UInt32 second_octet = 0x00ff0000;
        private const UInt32 third_octet = 0x0000ff00;
        private const UInt32 fourth_octet = 0x000000ff;

        /// Added by: Manuel
        /// <summary>
        /// Counts the number of set bits in the given bit field.
        /// </summary>
        /// <param name="unsignedInt32">The bit field to count the set bits in.</param>
        /// <returns>The number of set bits in the bit field.</returns>
        public static int count(UInt32 unsignedInt32)
        {
            // convert the 32 bit unsigned integer into each octet and look up the number of set bits per combination
            // 1. the 32 bit unsigned integer gets converted into 4 single bytes (each octet is one byte in size)
            // 2. each byte gets looked up in the lookup table how many bits are set
            // 3. the result is summed up and returned
            return
                lookupTable[(unsignedInt32 & first_octet ) >> 24] +
                lookupTable[(unsignedInt32 & second_octet) >> 16] +
                lookupTable[(unsignedInt32 & third_octet ) >>  8] +
                lookupTable[(unsignedInt32 & fourth_octet)      ];
        }

    }

}