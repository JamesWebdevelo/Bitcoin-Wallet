using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    /// <summary>
    /// Assertions class
    /// </summary>
    public static class Assertion
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static void AssertArgumentsLenght(int length, int min, int max)
        {
            if (length < min)
            {
                throw new Exception($"Not enough arguments are specified, minimum: {min}");

            }
            if (length > max)
            {
                throw new Exception($"Too many arguments are specified, maximum: {max}");
            }
        }
        #endregion
    }
}
