using System;
using System.Linq;

namespace TreeWarehouse.ViewModel.Utilities
{
    public static class RandomUtility
    {
        /// <summary>
        /// Get random string of chosen length. String generates from characters: ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <param name="length"> Chosen length. </param>
        /// <returns> Randomized string. </returns>
        public static string RandomString(this Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}